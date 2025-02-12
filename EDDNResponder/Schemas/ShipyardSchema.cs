﻿using EddiEddnResponder.Sender;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace EddiEddnResponder.Schemas
{
    [UsedImplicitly]
    public class ShipyardSchema : ISchema, ICapiSchema
    {
        public List<string> edTypes => new List<string> { "Shipyard" };

        // Track this so that we do not send duplicate data from the journal and from CAPI.
        private long? lastSentMarketID;
        private DateTime? lastSentDateTime;

        public bool Handle(string edType, ref IDictionary<string, object> data, EDDNState eddnState)
        {
            try
            {
                if (!edTypes.Contains(edType)) { return false; }
                if (eddnState?.GameVersion is null) { return false; }

                var marketID = JsonParsing.getLong(data, "MarketID");
                var timestamp = JsonParsing.getDateTime( "timestamp", data );

                // Suppress repetitious messages less than 2 minutes apart.
                if ( lastSentMarketID == marketID && timestamp < (lastSentDateTime + TimeSpan.FromMinutes(2)))
                {
                    return false;
                }
                lastSentMarketID = marketID;
                lastSentDateTime = timestamp;

                if (data.TryGetValue("PriceList", out var shipsList))
                {
                    // Only send the message if we have ships
                    if (shipsList is List<object> ships && ships.Any())
                    {
                        var handledData = new Dictionary<string, object>() as IDictionary<string, object>;
                        handledData["timestamp"] = data["timestamp"];
                        handledData["systemName"] = data["StarSystem"];
                        handledData["stationName"] = data["StationName"];
                        handledData["marketId"] = data["MarketID"];
                        handledData["allowCobraMkIV"] = data["AllowCobraMkIV"];
                        handledData["ships"] = ships
                            .Select(s => (s as Dictionary<string, object> ?? new Dictionary<string, object>())["ShipType"]?.ToString())
                            .Distinct()
                            .ToList();

                        // Apply data augments
                        handledData = eddnState.GameVersion.AugmentVersion(handledData);

                        EDDNSender.SendToEDDN("https://eddn.edcd.io/schemas/shipyard/2", handledData, eddnState);
                        data = handledData;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logging.Error($"{GetType().Name} failed to handle journal data.", e);
            }
            return false;
        }

        public IDictionary<string, object> Handle(JObject profileJson, JObject marketJson, JObject shipyardJson, JObject fleetCarrierJson, bool fromLegacyServer, EDDNState eddnState)
        {
            try
            {
                if (shipyardJson?["ships"] is null || eddnState?.GameVersion is null) { return null; }

                var systemName = profileJson?["lastSystem"]?["name"]?.ToString();
                var stationName = shipyardJson["name"].ToString();
                var marketID = shipyardJson["id"].ToObject<long>();
                var timestamp = shipyardJson["timestamp"].ToObject<DateTime?>();
                var allowCobraMkIV = profileJson?["commander"]?["capabilities"]?["AllowCobraMkIV"]?.ToObject<bool?>() ?? false;

                // Sanity check - we must have a valid timestamp
                if (timestamp == null) { return null; }

                // Suppress repetitious messages less than 2 minutes apart.
                if ( lastSentMarketID == marketID && timestamp < ( lastSentDateTime + TimeSpan.FromMinutes( 2 ) ) )
                {
                    return null;
                }

                // Build our ships list
                var ships = shipyardJson["ships"]?["shipyard_list"]?.Children().Values()
                    .Select(s => s["name"]?.ToString()).Distinct().ToList() ?? new List<string>();
                if (shipyardJson["ships"]["unavailable_list"] != null)
                {
                    ships.AddRange(shipyardJson["ships"]?["unavailable_list"]?
                        .Select(s => s?["name"]?.ToString()).Distinct().ToList() ?? new List<string>());
                }

                // Continue if our ships list is not empty
                if (ships.Any())
                {
                    var data = new Dictionary<string, object>() as IDictionary<string, object>;
                    data.Add("timestamp", timestamp);
                    data.Add("systemName", systemName);
                    data.Add("stationName", stationName);
                    data.Add("marketId", marketID);
                    data.Add("ships", ships);
                    data.Add("allowCobraMkIV", allowCobraMkIV);

                    // Apply data augments
                    data = eddnState.GameVersion.AugmentVersion(data);

                    var gameVersionOverride = fromLegacyServer ? "CAPI-Legacy-shipyard" : "CAPI-Live-shipyard";
                    EDDNSender.SendToEDDN("https://eddn.edcd.io/schemas/shipyard/2", data, eddnState, gameVersionOverride);
                    lastSentMarketID = marketID;
                    lastSentDateTime = timestamp;
                    return data;
                }
            }
            catch (Exception e)
            {
                Logging.Error($"{GetType().Name} failed to handle Frontier API data.", e);
            }

            return null;
        }
    }
}