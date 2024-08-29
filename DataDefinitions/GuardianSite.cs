using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utilities;


namespace EddiDataDefinitions
{
    public class GuardianSite /*: ResourceBasedLocalizedEDName<Nebula>*/
    {
        static GuardianSite ()
        {
            //resourceManager = Properties.OrganicGenus.ResourceManager;
            //resourceManager.IgnoreCase = true;
            //missingEDNameHandler = ( edname ) => new Nebula( NormalizeGenus( edname ) );
        }

        public static List<GuardianSite> AllOfThem = new List<GuardianSite> ();

        public enum GuardianSiteType {
            None = 0,
            Beacon = 1,
            Ruin = 2,
            Structure = 3
        }

        public enum BlueprintType {
            None = 0,
            Weapon = 1,
            Vessel = 2,
            Module = 3
        }

        public static readonly GuardianSite GB000 = new GuardianSite( GuardianSiteType.Beacon, "HIP 36823", "1", (decimal?)640.4375, (decimal?)-143.90625, (decimal?)-118.3125, BlueprintType.None);
        public static readonly GuardianSite GB001 = new GuardianSite( GuardianSiteType.Beacon, "IC 2391 Sector CQ-Y c16", "2", (decimal?)559.875, (decimal?)-87.15625, (decimal?)-33.15625, BlueprintType.None);
        public static readonly GuardianSite GB002 = new GuardianSite( GuardianSiteType.Beacon, "IC 2391 Sector FG-X b1-9", "8", (decimal?)563.75, (decimal?)-100.03125, (decimal?)-59.3125, BlueprintType.None);
        public static readonly GuardianSite GB003 = new GuardianSite( GuardianSiteType.Beacon, "IC 2391 Sector HG-X b1-7", "4", (decimal?)604.5625, (decimal?)-101.78125, (decimal?)-58.65625, BlueprintType.None);
        public static readonly GuardianSite GB004 = new GuardianSite( GuardianSiteType.Beacon, "IC 2391 Sector MX-T b3-6", "A 1", (decimal?)582.9375, (decimal?)-72.28125, (decimal?)-19.40625, BlueprintType.None);
        public static readonly GuardianSite GB005 = new GuardianSite( GuardianSiteType.Beacon, "NGC 2451A Sector LX-U d2-25", "3", (decimal?)726.0625, (decimal?)-163.5625, (decimal?)-171.78125, BlueprintType.None);
        public static readonly GuardianSite GB006 = new GuardianSite( GuardianSiteType.Beacon, "NGC 2451A Sector RT-R c4-19", "2", (decimal?)753.03125, (decimal?)-84.03125, (decimal?)-153.75, BlueprintType.None);
        public static readonly GuardianSite GB007 = new GuardianSite( GuardianSiteType.Beacon, "NGC 2451A Sector TO-R c4-3", "3", (decimal?)739.46875, (decimal?)-127.875, (decimal?)-156.1875, BlueprintType.None);
        public static readonly GuardianSite GB008 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe AS-H d11-9", "A 2", (decimal?)610.3125, (decimal?)-65.125, (decimal?)-101.84375, BlueprintType.None);
        public static readonly GuardianSite GB009 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe BO-G b44-6", "1", (decimal?)687.3125, (decimal?)-96.75, (decimal?)-112.125, BlueprintType.None);
        public static readonly GuardianSite GB010 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe CS-H d11-121", "3", (decimal?)746.09375, (decimal?)-100.78125, (decimal?)-86.625, BlueprintType.None);
        public static readonly GuardianSite GB011 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe DK-D b46-12", "2", (decimal?)636.3125, (decimal?)-53.65625, (decimal?)-68.8125, BlueprintType.None);
        public static readonly GuardianSite GB012 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe DK-P c22-35", "3", (decimal?)711.46875, (decimal?)-102.6875, (decimal?)-89.53125, BlueprintType.None);
        public static readonly GuardianSite GB013 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe FF-P c22-35", "4 a", (decimal?)700.03125, (decimal?)-129.5, (decimal?)-96.375, BlueprintType.None);
        public static readonly GuardianSite GB014 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe FU-E b45-5", "A 3", (decimal?)682.28125, (decimal?)-102.59375, (decimal?)-104.5625, BlueprintType.None);
        public static readonly GuardianSite GB015 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe GJ-G b44-9", "3 c", (decimal?)747.84375, (decimal?)-109.28125, (decimal?)-121.65625, BlueprintType.None);
        public static readonly GuardianSite GB016 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe HA-P c22-22", "2", (decimal?)701.5, (decimal?)-168.125, (decimal?)-104.40625, BlueprintType.None);
        public static readonly GuardianSite GB017 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe HL-N c23-4", "1 a + 1 b", (decimal?)649.9375, (decimal?)-114.78125, (decimal?)-57.0625, BlueprintType.None);
        public static readonly GuardianSite GB018 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe HP-E b45-4", "4", (decimal?)678.59375, (decimal?)-118.75, (decimal?)-94.78125, BlueprintType.None);
        public static readonly GuardianSite GB019 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe IJ-G b44-5", "3", (decimal?)790.0625, (decimal?)-124.8125, (decimal?)-120.40625, BlueprintType.None);
        public static readonly GuardianSite GB020 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe IL-N c23-15", "2 a", (decimal?)658.28125, (decimal?)-117.96875, (decimal?)-46.3125, BlueprintType.None);
        public static readonly GuardianSite GB021 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe IT-F d12-5", "A 2", (decimal?)739.46875, (decimal?)-140.53125, (decimal?)-13.375, BlueprintType.None);
        public static readonly GuardianSite GB022 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe JZ-F b44-8", "8", (decimal?)720.625, (decimal?)-163.59375, (decimal?)-112.90625, BlueprintType.None);
        public static readonly GuardianSite GB023 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe KU-F b44-4", "4", (decimal?)714.34375, (decimal?)-173.84375, (decimal?)-113.59375, BlueprintType.None);
        public static readonly GuardianSite GB024 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe OF-E b45-4", "1 b", (decimal?)751.0625, (decimal?)-157.71875, (decimal?)-86.09375, BlueprintType.None);
        public static readonly GuardianSite GB025 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe QA-E b45-4", "3", (decimal?)738.0625, (decimal?)-174.375, (decimal?)-103.25, BlueprintType.None);
        public static readonly GuardianSite GB026 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe RA-E b45-5", "2", (decimal?)755.53125, (decimal?)-169.125, (decimal?)-88.28125, BlueprintType.None);
        public static readonly GuardianSite GB027 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe RL-C b46-6", "3", (decimal?)727.90625, (decimal?)-157.1875, (decimal?)-67.1875, BlueprintType.None);
        public static readonly GuardianSite GB028 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe WL-J d10-113", "3", (decimal?)645.0625, (decimal?)-95.46875, (decimal?)-142.4375, BlueprintType.None);
        public static readonly GuardianSite GB029 = new GuardianSite( GuardianSiteType.Beacon, "Synuefe ZG-J d10-49", "2 a", (decimal?)723.8125, (decimal?)-156.71875, (decimal?)-152.3125, BlueprintType.None);
        public static readonly GuardianSite GR000 = new GuardianSite( GuardianSiteType.Ruin, "2MASS J10444160-5947046", "1 b", (decimal?)8614.1875, (decimal?)-116.6875, (decimal?)2733.03125, BlueprintType.None);
        public static readonly GuardianSite GR001 = new GuardianSite( GuardianSiteType.Ruin, "Blaa Hypai BN-I b26-1", "B 4", (decimal?)1290.3125, (decimal?)-666.375, (decimal?)12299.59375, BlueprintType.None);
        public static readonly GuardianSite GR002 = new GuardianSite( GuardianSiteType.Ruin, "Blaa Hypai EU-D c13-0", "B 4", (decimal?)1244.25, (decimal?)-752.1875, (decimal?)12307, BlueprintType.None);
        public static readonly GuardianSite GR003 = new GuardianSite( GuardianSiteType.Ruin, "Blaa Hypai LZ-F b27-0", "A 5", (decimal?)1251.4375, (decimal?)-752.71875, (decimal?)12330.53125, BlueprintType.None);
        public static readonly GuardianSite GR004 = new GuardianSite( GuardianSiteType.Ruin, "Blaa Hypai OZ-O d6-16", "4 c", (decimal?)1285.5, (decimal?)-731.65625, (decimal?)12309.34375, BlueprintType.None);
        public static readonly GuardianSite GR005 = new GuardianSite( GuardianSiteType.Ruin, "Blaa Hypai PB-A b31-0", "B 5", (decimal?)1239.5, (decimal?)-642.5625, (decimal?)12398.03125, BlueprintType.None);
        public static readonly GuardianSite GR006 = new GuardianSite( GuardianSiteType.Ruin, "Blaa Hypai XR-I b26-0", "A 3", (decimal?)1238.59375, (decimal?)-663.0625, (decimal?)12299.96875, BlueprintType.None);
        public static readonly GuardianSite GR007 = new GuardianSite( GuardianSiteType.Ruin, "Blae Eork IF-G c27-13", "A 1", (decimal?)8668.1875, (decimal?)-172.03125, (decimal?)2656.125, BlueprintType.None);
        public static readonly GuardianSite GR008 = new GuardianSite( GuardianSiteType.Ruin, "Blae Eork NE-E d13-25", "B 2", (decimal?)8670.0625, (decimal?)-65.1875, (decimal?)2680.59375, BlueprintType.None);
        public static readonly GuardianSite GR009 = new GuardianSite( GuardianSiteType.Ruin, "Blae Eork QU-D d13-3", "3", (decimal?)8602.75, (decimal?)-219.8125, (decimal?)2641.9375, BlueprintType.None);
        public static readonly GuardianSite GR010 = new GuardianSite( GuardianSiteType.Ruin, "Blae Eork RU-D d13-17", "1 a", (decimal?)8675.5, (decimal?)-238.5, (decimal?)2655.625, BlueprintType.None);
        public static readonly GuardianSite GR011 = new GuardianSite( GuardianSiteType.Ruin, "Blae Eork RU-D d13-20", "5 d", (decimal?)8675.25, (decimal?)-185.65625, (decimal?)2687.09375, BlueprintType.None);
        public static readonly GuardianSite GR012 = new GuardianSite( GuardianSiteType.Ruin, "Blae Eork UL-J b56-1", "A 3", (decimal?)8710.15625, (decimal?)-113.9375, (decimal?)2702, BlueprintType.None);
        public static readonly GuardianSite GR013 = new GuardianSite( GuardianSiteType.Ruin, "Blae Hypue DA-P d6-6", "4 a", (decimal?)1142.65625, (decimal?)-697.15625, (decimal?)12345.375, BlueprintType.None);
        public static readonly GuardianSite GR014 = new GuardianSite( GuardianSiteType.Ruin, "Blae Hypue KG-C c14-5", "B 1", (decimal?)1167.8125, (decimal?)-711.375, (decimal?)12365.28125, BlueprintType.None);
        public static readonly GuardianSite GR015 = new GuardianSite( GuardianSiteType.Ruin, "Col 132 Sector GS-K d8-33", "D 4", (decimal?)1111.5, (decimal?)-379.75, (decimal?)-418.3125, BlueprintType.None);
        public static readonly GuardianSite GR016 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector AI-H c11-22", "B 1", (decimal?)992.6875, (decimal?)-153.625, (decimal?)-213.71875, BlueprintType.None);
        public static readonly GuardianSite GR017 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector AP-Q b21-2", "A 8", (decimal?)1127.3125, (decimal?)-154.03125, (decimal?)-237.90625, BlueprintType.None);
        public static readonly GuardianSite GR018 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector AP-Q b21-2", "B 4", (decimal?)1127.3125, (decimal?)-154.03125, (decimal?)-237.90625, BlueprintType.None);
        public static readonly GuardianSite GR019 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector AU-Y b30-0", "B 2", (decimal?)1334.75, (decimal?)-169.53125, (decimal?)-33.46875, BlueprintType.None);
        public static readonly GuardianSite GR020 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector AU-Y b30-2", "B 4", (decimal?)1329.46875, (decimal?)-178.09375, (decimal?)-25.40625, BlueprintType.None);
        public static readonly GuardianSite GR021 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector AV-N b23-5", "A 2", (decimal?)1117.03125, (decimal?)-71.03125, (decimal?)-202.15625, BlueprintType.None);
        public static readonly GuardianSite GR022 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector BX-I c10-7", "B 2", (decimal?)1110.40625, (decimal?)-223.625, (decimal?)-242.6875, BlueprintType.None);
        public static readonly GuardianSite GR023 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector BX-I c10-7", "B 3", (decimal?)1110.40625, (decimal?)-223.625, (decimal?)-242.6875, BlueprintType.None);
        public static readonly GuardianSite GR024 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector CG-M b24-8", "A 4", (decimal?)1127.9375, (decimal?)-59.9375, (decimal?)-175.78125, BlueprintType.None);
        public static readonly GuardianSite GR025 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector CG-M b24-8", "A 5", (decimal?)1127.9375, (decimal?)-59.9375, (decimal?)-175.78125, BlueprintType.None);
        public static readonly GuardianSite GR026 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector CH-L d8-14", "D 1", (decimal?)1137.96875, (decimal?)-193.65625, (decimal?)-18.375, BlueprintType.None);
        public static readonly GuardianSite GR027 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector CH-L d8-14", "D 3", (decimal?)1137.96875, (decimal?)-193.65625, (decimal?)-18.375, BlueprintType.None);
        public static readonly GuardianSite GR028 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector DH-K b25-2", "A 5", (decimal?)1027.09375, (decimal?)-80.25, (decimal?)-163.4375, BlueprintType.None);
        public static readonly GuardianSite GR029 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector DH-K b25-2", "A 7", (decimal?)1027.09375, (decimal?)-80.25, (decimal?)-163.4375, BlueprintType.None);
        public static readonly GuardianSite GR030 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector DM-L c8-0", "B 2", (decimal?)1100.53125, (decimal?)-425, (decimal?)-341.84375, BlueprintType.None);
        public static readonly GuardianSite GR031 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector DM-L c8-0", "B 3", (decimal?)1100.53125, (decimal?)-425, (decimal?)-341.84375, BlueprintType.None);
        public static readonly GuardianSite GR032 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector DY-G b40-4", "D 1", (decimal?)1062.34375, (decimal?)-198.34375, (decimal?)170.84375, BlueprintType.None);
        public static readonly GuardianSite GR033 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector EC-L d8-54", "1 c", (decimal?)1180.5625, (decimal?)-303.34375, (decimal?)-14.09375, BlueprintType.None);
        public static readonly GuardianSite GR034 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector EH-L d8-41", "1 a", (decimal?)1333.75, (decimal?)-205.84375, (decimal?)-9.59375, BlueprintType.None);
        public static readonly GuardianSite GR035 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector EM-L d8-27", "4 a", (decimal?)1480.5, (decimal?)-182.3125, (decimal?)8, BlueprintType.None);
        public static readonly GuardianSite GR036 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector EM-L d8-60", "8 a", (decimal?)1511.21875, (decimal?)-177.4375, (decimal?)7.40625, BlueprintType.None);
        public static readonly GuardianSite GR037 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector FC-L d8-28", "14 c", (decimal?)1231.09375, (decimal?)-307.21875, (decimal?)-10.96875, BlueprintType.None);
        public static readonly GuardianSite GR038 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector GH-L d8-31", "C 3", (decimal?)1519.6875, (decimal?)-214, (decimal?)-11.3125, BlueprintType.None);
        public static readonly GuardianSite GR039 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector GM-V d2-3", "AB 1 b", (decimal?)1000.28125, (decimal?)-383.25, (decimal?)-444.8125, BlueprintType.None);
        public static readonly GuardianSite GR040 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector GM-V d2-3", "AB 1 c", (decimal?)1000.28125, (decimal?)-383.25, (decimal?)-444.8125, BlueprintType.None);
        public static readonly GuardianSite GR041 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector HI-M a55-0", "A 2", (decimal?)1147.4375, (decimal?)-43.9375, (decimal?)-113.21875, BlueprintType.None);
        public static readonly GuardianSite GR042 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector HI-M a55-0", "A 3", (decimal?)1147.4375, (decimal?)-43.9375, (decimal?)-113.21875, BlueprintType.None);
        public static readonly GuardianSite GR043 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector HR-M b23-3", "B 7", (decimal?)1024.28125, (decimal?)-191.71875, (decimal?)-193.8125, BlueprintType.Weapon);
        public static readonly GuardianSite GR044 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector HR-M b23-3", "C 5", (decimal?)1024.28125, (decimal?)-191.71875, (decimal?)-193.8125, BlueprintType.None);
        public static readonly GuardianSite GR045 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector ID-Z c14-0", "A 4", (decimal?)1228.90625, (decimal?)-401.4375, (decimal?)-26.96875, BlueprintType.None);
        public static readonly GuardianSite GR046 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector IE-R b34-3", "B 2", (decimal?)1563.8125, (decimal?)-244.625, (decimal?)39.34375, BlueprintType.None);
        public static readonly GuardianSite GR047 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector IY-H d10-54", "B 9", (decimal?)1153.15625, (decimal?)-158.53125, (decimal?)186.90625, BlueprintType.None);
        public static readonly GuardianSite GR048 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JB-F b27-1", "A 4", (decimal?)1154.96875, (decimal?)-259.96875, (decimal?)-113.46875, BlueprintType.None);
        public static readonly GuardianSite GR049 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JB-F b27-1", "BC 3", (decimal?)1154.96875, (decimal?)-259.96875, (decimal?)-113.46875, BlueprintType.None);
        public static readonly GuardianSite GR050 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JE-V b33-4", "B 2", (decimal?)1102.84375, (decimal?)13.25, (decimal?)20.0625, BlueprintType.None);
        public static readonly GuardianSite GR051 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JS-J d9-76", "14 b", (decimal?)1543.28125, (decimal?)-181.625, (decimal?)86.3125, BlueprintType.None);
        public static readonly GuardianSite GR052 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JS-J d9-76", "14 c", (decimal?)1543.28125, (decimal?)-181.625, (decimal?)86.3125, BlueprintType.None);
        public static readonly GuardianSite GR053 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JX-K b24-0", "B 2", (decimal?)993.0625, (decimal?)-188.1875, (decimal?)-173.53125, BlueprintType.None);
        public static readonly GuardianSite GR054 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector JX-K b24-0", "B 4", (decimal?)993.0625, (decimal?)-188.1875, (decimal?)-173.53125, BlueprintType.None);
        public static readonly GuardianSite GR055 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KN-J b25-5", "C 3", (decimal?)1002.90625, (decimal?)-152.28125, (decimal?)-160.25, BlueprintType.None);
        public static readonly GuardianSite GR056 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KN-J d9-22", "5 a", (decimal?)1514.8125, (decimal?)-263.875, (decimal?)75.125, BlueprintType.None);
        public static readonly GuardianSite GR057 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KP-T b34-8", "B 2", (decimal?)1086.09375, (decimal?)25.6875, (decimal?)46.3125, BlueprintType.None);
        public static readonly GuardianSite GR058 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KT-G b40-2", "A 3", (decimal?)1172.03125, (decimal?)-221.34375, (decimal?)167.28125, BlueprintType.None);
        public static readonly GuardianSite GR059 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KY-Q d5-47", "8 c", (decimal?)1043.875, (decimal?)-100.75, (decimal?)-246.0625, BlueprintType.None);
        public static readonly GuardianSite GR060 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KY-Q d5-47", "9 a", (decimal?)1043.875, (decimal?)-100.75, (decimal?)-246.0625, BlueprintType.None);
        public static readonly GuardianSite GR061 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector KY-Q d5-47", "9 d", (decimal?)1043.875, (decimal?)-100.75, (decimal?)-246.0625, BlueprintType.None);
        public static readonly GuardianSite GR062 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LB-W b31-0", "C 1", (decimal?)1258.5, (decimal?)-283.125, (decimal?)-20.09375, BlueprintType.None);
        public static readonly GuardianSite GR063 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LJ-F c12-0", "B 6", (decimal?)1202.125, (decimal?)-213.40625, (decimal?)-165.5625, BlueprintType.None);
        public static readonly GuardianSite GR064 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LJ-F c12-0", "B 7", (decimal?)1202.125, (decimal?)-213.40625, (decimal?)-165.5625, BlueprintType.None);
        public static readonly GuardianSite GR065 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LY-Q d5-13", "AB 9 a", (decimal?)1120.34375, (decimal?)-87.21875, (decimal?)-216.875, BlueprintType.None);
        public static readonly GuardianSite GR066 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LY-Q d5-13", "AB 9 b", (decimal?)1120.34375, (decimal?)-87.21875, (decimal?)-216.875, BlueprintType.None);
        public static readonly GuardianSite GR067 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LY-Q d5-59", "8 a", (decimal?)1078.09375, (decimal?)-86.5625, (decimal?)-249.46875, BlueprintType.None);
        public static readonly GuardianSite GR068 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector LY-Q d5-59", "8 b", (decimal?)1078.09375, (decimal?)-86.5625, (decimal?)-249.46875, BlueprintType.None);
        public static readonly GuardianSite GR069 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector MK-D c13-3", "B 2", (decimal?)1005.375, (decimal?)-235.78125, (decimal?)-138.15625, BlueprintType.None);
        public static readonly GuardianSite GR070 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OC-C b29-1", "A 4", (decimal?)1206.46875, (decimal?)-190.59375, (decimal?)-82.625, BlueprintType.None);
        public static readonly GuardianSite GR071 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OE-P d6-11", "B 3", (decimal?)1014.34375, (decimal?)-67.59375, (decimal?)-173.96875, BlueprintType.None);
        public static readonly GuardianSite GR072 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OE-P d6-11", "C 2", (decimal?)1014.34375, (decimal?)-67.59375, (decimal?)-173.96875, BlueprintType.None);
        public static readonly GuardianSite GR073 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OG-Z c15-35", "B 1", (decimal?)1084.125, (decimal?)2.59375, (decimal?)12.9375, BlueprintType.None);
        public static readonly GuardianSite GR074 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OO-Q d5-18", "6 a", (decimal?)1020.90625, (decimal?)-213.65625, (decimal?)-209.15625, BlueprintType.None);
        public static readonly GuardianSite GR075 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OO-Q d5-18", "6 b", (decimal?)1020.90625, (decimal?)-213.65625, (decimal?)-209.15625, BlueprintType.None);
        public static readonly GuardianSite GR076 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OP-E b41-1", "B 1", (decimal?)1079.71875, (decimal?)-247.875, (decimal?)176.90625, BlueprintType.None);
        public static readonly GuardianSite GR077 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OT-Q d5-18", "D 2", (decimal?)1150.75, (decimal?)-124.03125, (decimal?)-216.8125, BlueprintType.None);
        public static readonly GuardianSite GR078 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector OZ-U b33-6", "A 1", (decimal?)1160.90625, (decimal?)-20.03125, (decimal?)21.21875, BlueprintType.None);
        public static readonly GuardianSite GR079 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector PD-B b29-4", "A 5", (decimal?)1032.34375, (decimal?)-297.9375, (decimal?)-67.6875, BlueprintType.None);
        public static readonly GuardianSite GR080 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector PD-B b29-4", "A 6", (decimal?)1032.34375, (decimal?)-297.9375, (decimal?)-67.6875, BlueprintType.None);
        public static readonly GuardianSite GR081 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector PF-C c14-22", "B 2", (decimal?)1212.90625, (decimal?)-142.78125, (decimal?)-70.90625, BlueprintType.None);
        public static readonly GuardianSite GR082 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector PF-E b28-3", "B 1", (decimal?)870.34375, (decimal?)-156.03125, (decimal?)-92.84375, BlueprintType.Weapon);
        public static readonly GuardianSite GR083 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector PV-B c14-1", "D 2", (decimal?)1023.65625, (decimal?)-217.40625, (decimal?)-81.09375, BlueprintType.None);
        public static readonly GuardianSite GR084 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector QU-O d6-25", "5 b", (decimal?)878.84375, (decimal?)-205.5625, (decimal?)-156.59375, BlueprintType.None);
        public static readonly GuardianSite GR085 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector RF-D c13-12", "C 3", (decimal?)1108.5, (decimal?)-271.90625, (decimal?)-123.4375, BlueprintType.None);
        public static readonly GuardianSite GR086 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector RH-C b29-1", "B 3", (decimal?)1306.15625, (decimal?)-179.21875, (decimal?)-71.71875, BlueprintType.None);
        public static readonly GuardianSite GR087 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector RI-R b21-3", "C 1", (decimal?)1099.8125, (decimal?)-66.71875, (decimal?)-237.25, BlueprintType.None);
        public static readonly GuardianSite GR088 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector SA-C c14-1", "A 5", (decimal?)1216.21875, (decimal?)-182.125, (decimal?)-103.0625, BlueprintType.None);
        public static readonly GuardianSite GR089 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector SA-C c14-1", "B 4", (decimal?)1216.21875, (decimal?)-182.125, (decimal?)-103.0625, BlueprintType.None);
        public static readonly GuardianSite GR090 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector SB-Z c15-7", "B 4", (decimal?)1156.03125, (decimal?)-29.9375, (decimal?)-3.625, BlueprintType.None);
        public static readonly GuardianSite GR091 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UG-B c14-0", "C 1", (decimal?)1012.8125, (decimal?)-328.71875, (decimal?)-77.96875, BlueprintType.None);
        public static readonly GuardianSite GR092 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UM-X b16-0", "E 1", (decimal?)1005.625, (decimal?)-355.15625, (decimal?)-339.15625, BlueprintType.None);
        public static readonly GuardianSite GR093 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UU-O d6-20", "6 c", (decimal?)1192.09375, (decimal?)-209.15625, (decimal?)-184.65625, BlueprintType.None);
        public static readonly GuardianSite GR094 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UU-O d6-20", "6 d", (decimal?)1192.09375, (decimal?)-209.15625, (decimal?)-184.65625, BlueprintType.None);
        public static readonly GuardianSite GR095 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UU-O d6-30", "4 d", (decimal?)1196.6875, (decimal?)-225.84375, (decimal?)-161.5, BlueprintType.None);
        public static readonly GuardianSite GR096 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UU-O d6-30", "4 f", (decimal?)1196.6875, (decimal?)-225.84375, (decimal?)-161.5, BlueprintType.None);
        public static readonly GuardianSite GR097 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector UU-O d6-42", "A 1 c", (decimal?)1147.09375, (decimal?)-252.8125, (decimal?)-156.65625, BlueprintType.None);
        public static readonly GuardianSite GR098 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector VN-A b30-1", "C 3", (decimal?)1305.1875, (decimal?)-169.125, (decimal?)-52.125, BlueprintType.None);
        public static readonly GuardianSite GR099 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector VV-C c13-7", "AB 9 a", (decimal?)1107.65625, (decimal?)-373.21875, (decimal?)-108.0625, BlueprintType.None);
        public static readonly GuardianSite GR100 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector WF-N d7-52", "B 2", (decimal?)1186.6875, (decimal?)-166.1875, (decimal?)-80.1875, BlueprintType.None);
        public static readonly GuardianSite GR101 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector WG-J c10-16", "B 1", (decimal?)1067.28125, (decimal?)-138.3125, (decimal?)-258.3125, BlueprintType.None);
        public static readonly GuardianSite GR102 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector WK-A a48-4", "A 3", (decimal?)1098.15625, (decimal?)-39.96875, (decimal?)-188.75, BlueprintType.None);
        public static readonly GuardianSite GR103 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector WN-B b29-1", "D 1", (decimal?)1237.75, (decimal?)-247.375, (decimal?)-76.90625, BlueprintType.None);
        public static readonly GuardianSite GR104 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector WZ-O b22-4", "C 5", (decimal?)1011.0625, (decimal?)-131.78125, (decimal?)-210.4375, BlueprintType.None);
        public static readonly GuardianSite GR105 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector WZ-O b22-4", "C 6", (decimal?)1011.0625, (decimal?)-131.78125, (decimal?)-210.4375, BlueprintType.None);
        public static readonly GuardianSite GR106 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector XF-N d7-105", "A 4 a", (decimal?)1293.5625, (decimal?)-175.625, (decimal?)-48.3125, BlueprintType.None);
        public static readonly GuardianSite GR107 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector XG-J c10-17", "A 2", (decimal?)1095.25, (decimal?)-127.5625, (decimal?)-238.40625, BlueprintType.None);
        public static readonly GuardianSite GR108 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector XG-J c10-17", "B 4", (decimal?)1095.25, (decimal?)-127.5625, (decimal?)-238.40625, BlueprintType.None);
        public static readonly GuardianSite GR109 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector XY-Y b30-1", "B 2", (decimal?)1310.53125, (decimal?)-155.5, (decimal?)-27.65625, BlueprintType.None);
        public static readonly GuardianSite GR110 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YA-N d7-17", "A 2", (decimal?)1145.09375, (decimal?)-245.8125, (decimal?)-90.03125, BlueprintType.None);
        public static readonly GuardianSite GR111 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YA-N d7-17", "B 2", (decimal?)1145.09375, (decimal?)-245.8125, (decimal?)-90.03125, BlueprintType.None);
        public static readonly GuardianSite GR112 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YF-N c7-5", "B 2", (decimal?)1073.21875, (decimal?)-392.25, (decimal?)-378.3125, BlueprintType.None);
        public static readonly GuardianSite GR113 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YF-N c7-5", "B 3", (decimal?)1073.21875, (decimal?)-392.25, (decimal?)-378.3125, BlueprintType.None);
        public static readonly GuardianSite GR114 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YF-N d7-74", "C 4", (decimal?)1315.75, (decimal?)-181.5625, (decimal?)-62.1875, BlueprintType.None);
        public static readonly GuardianSite GR115 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YG-A c15-12", "B 1", (decimal?)1301.71875, (decimal?)-172.84375, (decimal?)-39.375, BlueprintType.None);
        public static readonly GuardianSite GR116 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YG-A c15-20", "A 1", (decimal?)1315.1875, (decimal?)-180.9375, (decimal?)-63.8125, BlueprintType.None);
        public static readonly GuardianSite GR117 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector YV-M d7-23", "A 7 a", (decimal?)1005.46875, (decimal?)-271.125, (decimal?)-76.625, BlueprintType.None);
        public static readonly GuardianSite GR118 = new GuardianSite( GuardianSiteType.Ruin, "Col 173 Sector ZK-O d6-24", "3 a", (decimal?)1250.0625, (decimal?)-397.9375, (decimal?)-135.90625, BlueprintType.None);
        public static readonly GuardianSite GR119 = new GuardianSite( GuardianSiteType.Ruin, "Drokoe FU-O b39-0", "C 7", (decimal?)14600.21875, (decimal?)-160.15625, (decimal?)3619.3125, BlueprintType.None);
        public static readonly GuardianSite GR120 = new GuardianSite( GuardianSiteType.Ruin, "Drokoe ML-M b40-0", "C 1", (decimal?)14539.03125, (decimal?)-222.3125, (decimal?)3635.96875, BlueprintType.None);
        public static readonly GuardianSite GR121 = new GuardianSite( GuardianSiteType.Ruin, "Drokoe ML-M b40-0", "C 2", (decimal?)14539.03125, (decimal?)-222.3125, (decimal?)3635.96875, BlueprintType.None);
        public static readonly GuardianSite GR122 = new GuardianSite( GuardianSiteType.Ruin, "Drokoe SB-M b40-0", "C 2", (decimal?)14591.0625, (decimal?)-252.28125, (decimal?)3646.1875, BlueprintType.None);
        public static readonly GuardianSite GR123 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-3468", "ABC 1 h", (decimal?)4948.46875, (decimal?)165.0625, (decimal?)20654.53125, BlueprintType.None);
        public static readonly GuardianSite GR124 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-3468", "ABC 3 c", (decimal?)4948.46875, (decimal?)165.0625, (decimal?)20654.53125, BlueprintType.None);
        public static readonly GuardianSite GR125 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-3732", "9 a", (decimal?)4958.9375, (decimal?)163.9375, (decimal?)20635.09375, BlueprintType.None);
        public static readonly GuardianSite GR126 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-3732", "9 b", (decimal?)4958.9375, (decimal?)163.9375, (decimal?)20635.09375, BlueprintType.None);
        public static readonly GuardianSite GR127 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-3861", "3 c", (decimal?)4950.3125, (decimal?)166.8125, (decimal?)20641.59375, BlueprintType.None);
        public static readonly GuardianSite GR128 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-3861", "4 c", (decimal?)4950.3125, (decimal?)166.8125, (decimal?)20641.59375, BlueprintType.None);
        public static readonly GuardianSite GR129 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy SY-Z d13-450", "1 a", (decimal?)4962.4375, (decimal?)139.15625, (decimal?)20636.40625, BlueprintType.None);
        public static readonly GuardianSite GR130 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy YF-Y b56-16", "B 3", (decimal?)4953.46875, (decimal?)155.8125, (decimal?)20635.6875, BlueprintType.None);
        public static readonly GuardianSite GR131 = new GuardianSite( GuardianSiteType.Ruin, "Eorl Auwsy YF-Y b56-16", "C 3", (decimal?)4953.46875, (decimal?)155.8125, (decimal?)20635.6875, BlueprintType.None);
        public static readonly GuardianSite GR132 = new GuardianSite( GuardianSiteType.Ruin, "Eta Carina Sector EL-Y d16", "ABC 4 a", (decimal?)8636.59375, (decimal?)-156.1875, (decimal?)2686.71875, BlueprintType.None);
        public static readonly GuardianSite GR133 = new GuardianSite( GuardianSiteType.Ruin, "Eta Carina Sector IM-V c2-4", "A 2", (decimal?)8629.1875, (decimal?)-98.8125, (decimal?)2713.0625, BlueprintType.None);
        public static readonly GuardianSite GR134 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AA-Z e24", "12 b", (decimal?)-632.5, (decimal?)-562.09375, (decimal?)13237, BlueprintType.None);
        public static readonly GuardianSite GR135 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AA-Z e24", "12 c", (decimal?)-632.5, (decimal?)-562.09375, (decimal?)13237, BlueprintType.None);
        public static readonly GuardianSite GR136 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-25", "A 7 a", (decimal?)-698.9375, (decimal?)-324.46875, (decimal?)13368.1875, BlueprintType.None);
        public static readonly GuardianSite GR137 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-25", "A 10 b", (decimal?)-698.9375, (decimal?)-324.46875, (decimal?)13368.1875, BlueprintType.None);
        public static readonly GuardianSite GR138 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-254", "A 6 b", (decimal?)-676.25, (decimal?)-312.5625, (decimal?)13345.84375, BlueprintType.None);
        public static readonly GuardianSite GR139 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-254", "A 8 a", (decimal?)-676.25, (decimal?)-312.5625, (decimal?)13345.84375, BlueprintType.None);
        public static readonly GuardianSite GR140 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-60", "9 a", (decimal?)-663.21875, (decimal?)-310.1875, (decimal?)13335.65625, BlueprintType.None);
        public static readonly GuardianSite GR141 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-65", "7 a", (decimal?)-669.9375, (decimal?)-338.53125, (decimal?)13347.84375, BlueprintType.None);
        public static readonly GuardianSite GR142 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue AG-V d3-65", "8 b", (decimal?)-669.9375, (decimal?)-338.53125, (decimal?)13347.84375, BlueprintType.None);
        public static readonly GuardianSite GR143 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue BH-T d4-242", "B 5", (decimal?)-1094.09375, (decimal?)-374.78125, (decimal?)13416.03125, BlueprintType.None);
        public static readonly GuardianSite GR144 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue BH-T d4-242", "C 4", (decimal?)-1094.09375, (decimal?)-374.78125, (decimal?)13416.03125, BlueprintType.None);
        public static readonly GuardianSite GR145 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue BX-G b28-27", "B 1", (decimal?)-1041.78125, (decimal?)-483.125, (decimal?)13623.40625, BlueprintType.None);
        public static readonly GuardianSite GR146 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue DL-W d2-16", "3", (decimal?)-588, (decimal?)-539.5625, (decimal?)13287.875, BlueprintType.None);
        public static readonly GuardianSite GR147 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue GH-M b11-1", "B 1", (decimal?)-624.4375, (decimal?)-518.0315, (decimal?)13258.4375, BlueprintType.None);
        public static readonly GuardianSite GR148 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue KI-K b12-13", "B 1", (decimal?)-649.625, (decimal?)-537.53125, (decimal?)13286.3125, BlueprintType.None);
        public static readonly GuardianSite GR149 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LO-P d6-194", "14 a", (decimal?)-1037.65625, (decimal?)-492.65625, (decimal?)13612.6875, BlueprintType.None);
        public static readonly GuardianSite GR150 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LO-P d6-194", "14 b", (decimal?)-1037.65625, (decimal?)-492.65625, (decimal?)13612.6875, BlueprintType.None);
        public static readonly GuardianSite GR151 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LS-S d4-3", "4 a", (decimal?)-726.96875, (decimal?)-589.90625, (decimal?)13438.40625, BlueprintType.None);
        public static readonly GuardianSite GR152 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LS-S d4-3", "6 a", (decimal?)-726.96875, (decimal?)-589.90625, (decimal?)13438.40625, BlueprintType.None);
        public static readonly GuardianSite GR153 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LS-S d4-81", "AB 5 a", (decimal?)-760.8125, (decimal?)-641.09375, (decimal?)13431.65625, BlueprintType.None);
        public static readonly GuardianSite GR154 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LS-S d4-81", "AB 6 b", (decimal?)-760.8125, (decimal?)-641.09375, (decimal?)13431.65625, BlueprintType.None);
        public static readonly GuardianSite GR155 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue LS-S d4-81", "AB 6 c", (decimal?)-760.8125, (decimal?)-641.09375, (decimal?)13431.65625, BlueprintType.None);
        public static readonly GuardianSite GR156 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue NY-Q d5-111", "5 a", (decimal?)-898.625, (decimal?)-643.09375, (decimal?)13529.90625, BlueprintType.None);
        public static readonly GuardianSite GR157 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue NY-Q d5-118", "9 a", (decimal?)-915.25, (decimal?)-613.78125, (decimal?)13527.46875, BlueprintType.None);
        public static readonly GuardianSite GR158 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue NY-Q d5-148", "8 a", (decimal?)-922.84375, (decimal?)-621, (decimal?)13530.84375, BlueprintType.None);
        public static readonly GuardianSite GR159 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue OJ-W c3-34", "C 1", (decimal?)-999, (decimal?)-562.53125, (decimal?)13175.03125, BlueprintType.None);
        public static readonly GuardianSite GR160 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue OJ-W c3-34", "C 2", (decimal?)-999, (decimal?)-562.53125, (decimal?)13175.03125, BlueprintType.None);
        public static readonly GuardianSite GR161 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue PT-Q d5-122", "10 a", (decimal?)-885.34375, (decimal?)-698.71875, (decimal?)13551.34375, BlueprintType.None);
        public static readonly GuardianSite GR162 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue PT-Q d5-122", "10 b", (decimal?)-885.34375, (decimal?)-698.71875, (decimal?)13551.34375, BlueprintType.None);
        public static readonly GuardianSite GR163 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue PT-Q d5-97", "7 c", (decimal?)-905.25, (decimal?)-665.78125, (decimal?)13530.65625, BlueprintType.None);
        public static readonly GuardianSite GR164 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue PT-Q d5-99", "7 a", (decimal?)-915.71875, (decimal?)-690.9375, (decimal?)13541.0625, BlueprintType.None);
        public static readonly GuardianSite GR165 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QG-Q c7-17", "C 1", (decimal?)-1150.15625, (decimal?)-376.03125, (decimal?)13364.21875, BlueprintType.None);
        public static readonly GuardianSite GR166 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QG-Q c7-17", "C 2", (decimal?)-1150.15625, (decimal?)-376.03125, (decimal?)13364.21875, BlueprintType.None);
        public static readonly GuardianSite GR167 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QL-V b19-15", "C 3", (decimal?)-819.125, (decimal?)-623.34375, (decimal?)13440.4375, BlueprintType.None);
        public static readonly GuardianSite GR168 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QL-V b19-15", "C 4", (decimal?)-819.125, (decimal?)-623.34375, (decimal?)13440.4375, BlueprintType.None);
        public static readonly GuardianSite GR169 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QL-V b19-15", "D 2", (decimal?)-819.125, (decimal?)-623.34375, (decimal?)13440.4375, BlueprintType.None);
        public static readonly GuardianSite GR170 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QY-J b12-7", "B 4", (decimal?)-611.625, (decimal?)-575.4375, (decimal?)13282.71875, BlueprintType.None);
        public static readonly GuardianSite GR171 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QY-J b12-7", "B 5", (decimal?)-611.625, (decimal?)-575.4375, (decimal?)13282.71875, BlueprintType.None);
        public static readonly GuardianSite GR172 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QY-Z d179", "B 3", (decimal?)-988.71875, (decimal?)-564.5, (decimal?)13149.6875, BlueprintType.None);
        public static readonly GuardianSite GR173 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue QY-Z d179", "B 4", (decimal?)-988.71875, (decimal?)-564.5, (decimal?)13149.6875, BlueprintType.None);
        public static readonly GuardianSite GR174 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue UE-Y d1-58", "C 1", (decimal?)-1017.5, (decimal?)-524.3125, (decimal?)13231.71875, BlueprintType.None);
        public static readonly GuardianSite GR175 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue UE-Y d1-58", "C 5", (decimal?)-1017.5, (decimal?)-524.3125, (decimal?)13231.71875, BlueprintType.None);
        public static readonly GuardianSite GR176 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue UT-D b17-5", "C 3", (decimal?)-897.125, (decimal?)-300.15625, (decimal?)13392.1875, BlueprintType.None);
        public static readonly GuardianSite GR177 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue UT-D b17-5", "D 1", (decimal?)-897.125, (decimal?)-300.15625, (decimal?)13392.1875, BlueprintType.None);
        public static readonly GuardianSite GR178 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue VZ-X d1-92", "5 b", (decimal?)-1031.75, (decimal?)-606.375, (decimal?)13195.03125, BlueprintType.None);
        public static readonly GuardianSite GR179 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue WA-V d3-215", "B 1", (decimal?)-1157.1875, (decimal?)-405.53125, (decimal?)13376.25, BlueprintType.None);
        public static readonly GuardianSite GR180 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue WA-V d3-215", "C 1", (decimal?)-1157.1875, (decimal?)-405.53125, (decimal?)13376.25, BlueprintType.None);
        public static readonly GuardianSite GR181 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue YE-Y d1-212", "3 a", (decimal?)-637.6875, (decimal?)-577.125, (decimal?)13234.15625, BlueprintType.None);
        public static readonly GuardianSite GR182 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue YX-S b7-4", "B 2", (decimal?)-1049.78125, (decimal?)-555.6875, (decimal?)13175.25, BlueprintType.None);
        public static readonly GuardianSite GR183 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue YX-S b7-4", "B 3", (decimal?)-1049.78125, (decimal?)-555.6875, (decimal?)13175.25, BlueprintType.None);
        public static readonly GuardianSite GR184 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue ZR-O c8-117", "B 1", (decimal?)-900.6875, (decimal?)-339.625, (decimal?)13390.5, BlueprintType.None);
        public static readonly GuardianSite GR185 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue ZV-U d3-301", "A 9 a", (decimal?)-1093.59375, (decimal?)-432.25, (decimal?)13396.59375, BlueprintType.None);
        public static readonly GuardianSite GR186 = new GuardianSite( GuardianSiteType.Ruin, "Graea Hypue ZV-U d3-301", "A 9 c", (decimal?)-1093.59375, (decimal?)-432.25, (decimal?)13396.59375, BlueprintType.None);
        public static readonly GuardianSite GR187 = new GuardianSite( GuardianSiteType.Ruin, "HIP 39768", "A 14 f", (decimal?)866.59375, (decimal?)-119.125, (decimal?)-109.03125, BlueprintType.None);
        public static readonly GuardianSite GR188 = new GuardianSite( GuardianSiteType.Ruin, "IC 2391 Sector FL-X b1-7", "A 2", (decimal?)611.34375, (decimal?)-78.40625, (decimal?)-51.6875, BlueprintType.Vessel);
        public static readonly GuardianSite GR189 = new GuardianSite( GuardianSiteType.Ruin, "IC 2391 Sector GW-V b2-4", "B 1", (decimal?)587.9375, (decimal?)-51.03125, (decimal?)-38.53125, BlueprintType.None);
        public static readonly GuardianSite GR190 = new GuardianSite( GuardianSiteType.Ruin, "IC 2391 Sector YE-A d103", "B 1", (decimal?)489.03125, (decimal?)-98.09375, (decimal?)-34.96875, BlueprintType.None);
        public static readonly GuardianSite GR191 = new GuardianSite( GuardianSiteType.Ruin, "IC 2391 Sector ZE-A d101", "C 3", (decimal?)526.5, (decimal?)-86.375, (decimal?)-37.9375, BlueprintType.None);
        public static readonly GuardianSite GR192 = new GuardianSite( GuardianSiteType.Ruin, "NGC 2516 Sector UT-Z b2", "A 1", (decimal?)1294.3125, (decimal?)-322.46875, (decimal?)-13.78125, BlueprintType.None);
        public static readonly GuardianSite GR193 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector BV-Y c2", "B 1", (decimal?)14595.53125, (decimal?)-225.84375, (decimal?)3490.34375, BlueprintType.None);
        public static readonly GuardianSite GR194 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector CQ-Y d6", "B 4", (decimal?)14596.96875, (decimal?)-264.6875, (decimal?)3570.78125, BlueprintType.None);
        public static readonly GuardianSite GR195 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector DL-Y d12", "1 a", (decimal?)14563.875, (decimal?)-302.53125, (decimal?)3509.9375, BlueprintType.None);
        public static readonly GuardianSite GR196 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector IM-V c2-5", "B 3", (decimal?)14619.96875, (decimal?)-220.375, (decimal?)3560.375, BlueprintType.None);
        public static readonly GuardianSite GR197 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector IM-V c2-5", "B 4", (decimal?)14619.96875, (decimal?)-220.375, (decimal?)3560.375, BlueprintType.None);
        public static readonly GuardianSite GR198 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector MC-V c2-8", "A 5", (decimal?)14619.8125, (decimal?)-275.40625, (decimal?)3560.90625, BlueprintType.None);
        public static readonly GuardianSite GR199 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector XJ-A d10", "11 a", (decimal?)14544.0625, (decimal?)-237.625, (decimal?)3489.375, BlueprintType.None);
        public static readonly GuardianSite GR200 = new GuardianSite( GuardianSiteType.Ruin, "NGC 3199 Sector XJ-A d10", "13 b", (decimal?)14544.0625, (decimal?)-237.625, (decimal?)3489.375, BlueprintType.None);
        public static readonly GuardianSite GR201 = new GuardianSite( GuardianSiteType.Ruin, "Nyeajeou VP-G b56-0", "B 2", (decimal?)-9275.46875, (decimal?)-423.4375, (decimal?)7817.34375, BlueprintType.None);
        public static readonly GuardianSite GR202 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo GF-E c10", "B 4", (decimal?)-9308.09375, (decimal?)-406.1875, (decimal?)7927.40625, BlueprintType.None);
        public static readonly GuardianSite GR203 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo NQ-F b2-3", "C 1", (decimal?)-9324.5625, (decimal?)-339.03125, (decimal?)7954.53125, BlueprintType.None);
        public static readonly GuardianSite GR204 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo PC-C d50", "C 1", (decimal?)-9371.3125, (decimal?)-408.5, (decimal?)7899.71875, BlueprintType.None);
        public static readonly GuardianSite GR205 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo QC-C d31", "2 a", (decimal?)-9294.75, (decimal?)-379.9375, (decimal?)7952.03125, BlueprintType.None);
        public static readonly GuardianSite GR206 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo RC-C d19", "B 1", (decimal?)-9195.28125, (decimal?)-400.4375, (decimal?)7917.0625, BlueprintType.None);
        public static readonly GuardianSite GR207 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo WD-A d1-41", "5 c", (decimal?)-9326.09375, (decimal?)-425.46875, (decimal?)7995.59375, BlueprintType.None);
        public static readonly GuardianSite GR208 = new GuardianSite( GuardianSiteType.Ruin, "Prai Hypoo WD-A d1-6", "10 a", (decimal?)-9302.25, (decimal?)-459.46875, (decimal?)7978.78125, BlueprintType.None);
        public static readonly GuardianSite GR209 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe TS-B d252", "C 6", (decimal?)-5541.84375, (decimal?)-548.03125, (decimal?)10516.9375, BlueprintType.None);
        public static readonly GuardianSite GR210 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe TS-B d252", "D 1", (decimal?)-5541.84375, (decimal?)-548.03125, (decimal?)10516.9375, BlueprintType.None);
        public static readonly GuardianSite GR211 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe US-B d86", "C 2", (decimal?)-5476.6875, (decimal?)-518.8125, (decimal?)10526.65625, BlueprintType.None);
        public static readonly GuardianSite GR212 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe US-B d86", "C 3", (decimal?)-5476.6875, (decimal?)-518.8125, (decimal?)10526.65625, BlueprintType.None);
        public static readonly GuardianSite GR213 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe XY-Z d114", "4 d", (decimal?)-5517.78125, (decimal?)-566, (decimal?)10544.46875, BlueprintType.None);
        public static readonly GuardianSite GR214 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe XY-Z d114", "4 f", (decimal?)-5517.78125, (decimal?)-566, (decimal?)10544.46875, BlueprintType.None);
        public static readonly GuardianSite GR215 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe XY-Z d42", "7 a", (decimal?)-5525.5625, (decimal?)-559.09375, (decimal?)10547.71875, BlueprintType.None);
        public static readonly GuardianSite GR216 = new GuardianSite( GuardianSiteType.Ruin, "Prua Phoe XY-Z d42", "8 a", (decimal?)-5525.5625, (decimal?)-559.09375, (decimal?)10547.71875, BlueprintType.None);
        public static readonly GuardianSite GR217 = new GuardianSite( GuardianSiteType.Ruin, "Skaudai AM-B d14-138", "AB 7 a", (decimal?)-5477.59375, (decimal?)-504.15625, (decimal?)10436.25, BlueprintType.None);
        public static readonly GuardianSite GR218 = new GuardianSite( GuardianSiteType.Ruin, "Swoilz AE-F c13", "C 1", (decimal?)1079.3125, (decimal?)-216.125, (decimal?)222.96875, BlueprintType.None);
        public static readonly GuardianSite GR219 = new GuardianSite( GuardianSiteType.Ruin, "Swoilz PA-F b3-2", "B 2", (decimal?)1121.8125, (decimal?)-217.125, (decimal?)282.9375, BlueprintType.None);
        public static readonly GuardianSite GR220 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe CE-R c21-6", "C 1", (decimal?)828.1875, (decimal?)-78, (decimal?)-105.1875, BlueprintType.Weapon);
        public static readonly GuardianSite GR221 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe CN-H d11-63", "A 2 b", (decimal?)610.96875, (decimal?)-145.78125, (decimal?)-57.90625, BlueprintType.None);
        public static readonly GuardianSite GR222 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe DK-D b46-4", "C 1", (decimal?)652.9375, (decimal?)-63.90625, (decimal?)-80.78125, BlueprintType.Vessel);
        public static readonly GuardianSite GR223 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe LY-I b42-2", "C 2", (decimal?)814.71875, (decimal?)-222.78125, (decimal?)-151.15625, BlueprintType.None);
        public static readonly GuardianSite GR224 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe NL-N c23-4", "B 3", (decimal?)860.125, (decimal?)-124.59375, (decimal?)-61.0625, BlueprintType.Module);
        public static readonly GuardianSite GR225 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe TP-F b44-0", "CD 1", (decimal?)838.75, (decimal?)-197.84375, (decimal?)-111.84375, BlueprintType.None);
        public static readonly GuardianSite GR226 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe XO-P c22-17", "C 1", (decimal?)546.90625, (decimal?)-56.46875, (decimal?)-97.8125, BlueprintType.None);
        public static readonly GuardianSite GR227 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe XR-H d11-102", "1 b", (decimal?)357.34375, (decimal?)-49.34375, (decimal?)-74.75, BlueprintType.None);
        public static readonly GuardianSite GR228 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe YY-Q c21-19", "2 a", (decimal?)589.15625, (decimal?)-144.5, (decimal?)-107.84375, BlueprintType.None);
        public static readonly GuardianSite GR229 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe ZL-J d10-109", "E 3", (decimal?)852.65625, (decimal?)-51.125, (decimal?)-124.84375, BlueprintType.Weapon);
        public static readonly GuardianSite GR230 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe ZL-J d10-119", "9 b", (decimal?)834.21875, (decimal?)-51.21875, (decimal?)-154.65625, BlueprintType.None);
        public static readonly GuardianSite GR231 = new GuardianSite( GuardianSiteType.Ruin, "Synuefe ZR-I b43-10", "D 2", (decimal?)811.40625, (decimal?)-60.4375, (decimal?)-144.71875, BlueprintType.None);
        public static readonly GuardianSite GR232 = new GuardianSite( GuardianSiteType.Ruin, "Trapezium Sector YU-X c1-2", "1 a", (decimal?)573.59375, (decimal?)-339.46875, (decimal?)-1167.65625, BlueprintType.None);
        public static readonly GuardianSite GR233 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region EL-Y d32", "B 1", (decimal?)1000.65625, (decimal?)-166.21875, (decimal?)-64.15625, BlueprintType.None);
        public static readonly GuardianSite GR234 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region FL-Y d63", "A 5", (decimal?)1064.5, (decimal?)-144.03125, (decimal?)-101.71875, BlueprintType.None);
        public static readonly GuardianSite GR235 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region FL-Y d63", "B 1", (decimal?)1064.5, (decimal?)-144.03125, (decimal?)-101.71875, BlueprintType.None);
        public static readonly GuardianSite GR236 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region HB-X c1-28", "E 2", (decimal?)1073.0625, (decimal?)-100.65625, (decimal?)-92.75, BlueprintType.None);
        public static readonly GuardianSite GR237 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region HB-X c1-28", "E 3", (decimal?)1073.0625, (decimal?)-100.65625, (decimal?)-92.75, BlueprintType.None);
        public static readonly GuardianSite GR238 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region KR-W c1-24", "7 a", (decimal?)1036.875, (decimal?)-163.59375, (decimal?)-85.96875, BlueprintType.None);
        public static readonly GuardianSite GR239 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region KR-W c1-24", "7 c", (decimal?)1036.875, (decimal?)-163.59375, (decimal?)-85.96875, BlueprintType.None);
        public static readonly GuardianSite GR240 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region RC-V b2-5", "C 3", (decimal?)1072.75, (decimal?)-168.1875, (decimal?)-85.125, BlueprintType.None);
        public static readonly GuardianSite GR241 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region RC-V b2-5", "C 4", (decimal?)1072.75, (decimal?)-168.1875, (decimal?)-85.125, BlueprintType.None);
        public static readonly GuardianSite GR242 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region TD-S b4-5", "C 1", (decimal?)1071.21875, (decimal?)-121.03125, (decimal?)-50.09375, BlueprintType.None);
        public static readonly GuardianSite GR243 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region TD-S b4-5", "C 2", (decimal?)1071.21875, (decimal?)-121.03125, (decimal?)-50.09375, BlueprintType.None);
        public static readonly GuardianSite GR244 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region VO-Q b5-1", "B 6", (decimal?)1062.03125, (decimal?)-91.8125, (decimal?)-40.4375, BlueprintType.None);
        public static readonly GuardianSite GR245 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region VO-Q b5-1", "C 5", (decimal?)1062.03125, (decimal?)-91.8125, (decimal?)-40.4375, BlueprintType.None);
        public static readonly GuardianSite GR246 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region VO-Q b5-6", "A 1", (decimal?)1059.65625, (decimal?)-102.28125, (decimal?)-36.125, BlueprintType.None);
        public static readonly GuardianSite GR247 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region VO-Q b5-6", "A 2", (decimal?)1059.65625, (decimal?)-102.28125, (decimal?)-36.125, BlueprintType.None);
        public static readonly GuardianSite GR248 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region WJ-Q b5-8", "B 1", (decimal?)1036.6875, (decimal?)-117.75, (decimal?)-27.1875, BlueprintType.None);
        public static readonly GuardianSite GR249 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region WJ-Q b5-8", "B 2", (decimal?)1036.6875, (decimal?)-117.75, (decimal?)-27.1875, BlueprintType.None);
        public static readonly GuardianSite GR250 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region WT-R b4-3", "B 1", (decimal?)1044.6875, (decimal?)-158.4375, (decimal?)-62.5625, BlueprintType.None);
        public static readonly GuardianSite GR251 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region WT-R b4-3", "B 2", (decimal?)1044.6875, (decimal?)-158.4375, (decimal?)-62.5625, BlueprintType.None);
        public static readonly GuardianSite GR252 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region YO-R b4-1", "B 4", (decimal?)1045.625, (decimal?)-177.53125, (decimal?)-45.40625, BlueprintType.None);
        public static readonly GuardianSite GR253 = new GuardianSite( GuardianSiteType.Ruin, "Vela Dark Region YO-R b4-1", "D 2", (decimal?)1045.625, (decimal?)-177.53125, (decimal?)-45.40625, BlueprintType.None);
        public static readonly GuardianSite GS000 = new GuardianSite( GuardianSiteType.Structure, "Col 135 Sector TU-O c6-15", "D 1", (decimal?)923.9375, (decimal?)-138.125, (decimal?)-231.84375, BlueprintType.None);
        public static readonly GuardianSite GS001 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector AD-H c11-15", "B 2", (decimal?)925, (decimal?)-200.09375, (decimal?)-205.65625, BlueprintType.None);
        public static readonly GuardianSite GS002 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector AI-H c11-22", "B 1", (decimal?)992.6875, (decimal?)-153.625, (decimal?)-213.71875, BlueprintType.None);
        public static readonly GuardianSite GS003 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector AY-F c12-0", "A 1", (decimal?)988.90625, (decimal?)-78.6875, (decimal?)-155.65625, BlueprintType.None);
        public static readonly GuardianSite GS004 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector BH-K b25-8", "B 3", (decimal?)986.21875, (decimal?)-69.53125, (decimal?)-163.4375, BlueprintType.None);
        public static readonly GuardianSite GS005 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector BO-F c12-24", "A 3", (decimal?)864.5, (decimal?)-161.65625, (decimal?)-153.03125, BlueprintType.None);
        public static readonly GuardianSite GS006 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector CC-K b25-8", "A 5", (decimal?)970.375, (decimal?)-99.5625, (decimal?)-160.53125, BlueprintType.Weapon);
        public static readonly GuardianSite GS007 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector DE-E c13-27", "C 2", (decimal?)961.28125, (decimal?)-69.25, (decimal?)-139.5, BlueprintType.None);
        public static readonly GuardianSite GS008 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector DH-K b25-2", "A 5", (decimal?)1027.09375, (decimal?)-80.25, (decimal?)-163.4375, BlueprintType.None);
        public static readonly GuardianSite GS009 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector DO-F c12-4", "A 1", (decimal?)943.15625, (decimal?)-169.46875, (decimal?)-146.78125, BlueprintType.None);
        public static readonly GuardianSite GS010 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector DS-J b25-2", "B 5", (decimal?)895.875, (decimal?)-132.125, (decimal?)-150.71875, BlueprintType.None);
        public static readonly GuardianSite GS011 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector DZ-D c13-2", "12 a", (decimal?)864.0625, (decimal?)-122.4375, (decimal?)-136.375, BlueprintType.None);
        public static readonly GuardianSite GS012 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector EZ-D c13-25", "B 1", (decimal?)922.3125, (decimal?)-135.65625, (decimal?)-130.96875, BlueprintType.None);
        public static readonly GuardianSite GS013 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector FK-C c14-6", "D 2", (decimal?)869.46875, (decimal?)-84.46875, (decimal?)-93.5, BlueprintType.None);
        public static readonly GuardianSite GS014 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector FZ-D c13-30", "2", (decimal?)951.375, (decimal?)-117.09375, (decimal?)-143.8125, BlueprintType.Weapon);
        public static readonly GuardianSite GS015 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector GS-J b25-4", "D 2", (decimal?)957.03125, (decimal?)-142, (decimal?)-160.53125, BlueprintType.Module);
        public static readonly GuardianSite GS016 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector HN-I b26-5", "A 1", (decimal?)1022.3125, (decimal?)-76.9375, (decimal?)-139.03125, BlueprintType.Weapon);
        public static readonly GuardianSite GS017 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector HR-M b23-0", "C 1", (decimal?)1015.5625, (decimal?)-200.84375, (decimal?)-190.4375, BlueprintType.None);
        public static readonly GuardianSite GS018 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector HR-M b23-3", "B 7", (decimal?)1024.28125, (decimal?)-191.71875, (decimal?)-193.8125, BlueprintType.None);
        public static readonly GuardianSite GS019 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector HU-D c13-2", "A 1", (decimal?)940.84375, (decimal?)-173.9375, (decimal?)-126.28125, BlueprintType.None);
        public static readonly GuardianSite GS020 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector IJ-G b27-1", "A 1", (decimal?)882.65625, (decimal?)-122.875, (decimal?)-115.71875, BlueprintType.None);
        public static readonly GuardianSite GS021 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector JI-J b25-0", "AB 2", (decimal?)936.78125, (decimal?)-184.125, (decimal?)-160.75, BlueprintType.None);
        public static readonly GuardianSite GS022 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector JP-D c13-14", "A 4", (decimal?)948.0625, (decimal?)-216.9375, (decimal?)-107.65625, BlueprintType.None);
        public static readonly GuardianSite GS023 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector JX-K b24-0", "B 2", (decimal?)993.0625, (decimal?)-188.1875, (decimal?)-173.53125, BlueprintType.None);
        public static readonly GuardianSite GS024 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector KN-J b25-5", "C 3", (decimal?)1002.90625, (decimal?)-152.28125, (decimal?)-160.25, BlueprintType.None);
        public static readonly GuardianSite GS025 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector KY-Q d5-26", "6 a", (decimal?)975.3125, (decimal?)-103.1875, (decimal?)-222.125, BlueprintType.Weapon);
        public static readonly GuardianSite GS026 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector KY-Q d5-42", "1 a", (decimal?)991.875, (decimal?)-84.625, (decimal?)-203.71875, BlueprintType.None);
        public static readonly GuardianSite GS027 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector LT-Q d5-82", "A 3", (decimal?)926.84375, (decimal?)-120.65625, (decimal?)-199.6875, BlueprintType.Weapon);
        public static readonly GuardianSite GS028 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector LT-Q d5-90", "D 1", (decimal?)911.0625, (decimal?)-115.875, (decimal?)-208.15625, BlueprintType.Weapon);
        public static readonly GuardianSite GS029 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector ME-P d6-92", "B 5", (decimal?)891.65625, (decimal?)-98.53125, (decimal?)-157.1875, BlueprintType.Weapon);
        public static readonly GuardianSite GS030 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector NV-B c14-1", "1", (decimal?)954.21875, (decimal?)-212.5625, (decimal?)-94.8125, BlueprintType.Weapon);
        public static readonly GuardianSite GS031 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector OD-J b25-2", "B 1", (decimal?)1008.375, (decimal?)-201.5625, (decimal?)-157.4375, BlueprintType.Weapon);
        public static readonly GuardianSite GS032 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector PF-E b28-3", "B 1", (decimal?)870.34375, (decimal?)-156.03125, (decimal?)-92.84375, BlueprintType.None);
        public static readonly GuardianSite GS033 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector PZ-O d6-110", "5 a", (decimal?)943.0625, (decimal?)-130.75, (decimal?)-142.03125, BlueprintType.Weapon);
        public static readonly GuardianSite GS034 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector PZ-O d6-116", "6 d", (decimal?)931.4375, (decimal?)-131.25, (decimal?)-164.46875, BlueprintType.None);
        public static readonly GuardianSite GS035 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector QU-O d6-25", "5 b", (decimal?)878.84375, (decimal?)-205.5625, (decimal?)-156.59375, BlueprintType.None);
        public static readonly GuardianSite GS036 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector QU-O d6-32", "3 a", (decimal?)894.09375, (decimal?)-203.625, (decimal?)-146.5625, BlueprintType.None);
        public static readonly GuardianSite GS037 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector QZ-O d6-1", "4 c", (decimal?)989.28125, (decimal?)-170.8125, (decimal?)-175.125, BlueprintType.None);
        public static readonly GuardianSite GS038 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector RT-I b25-1", "B 1", (decimal?)977, (decimal?)-233.625, (decimal?)-151.0625, BlueprintType.None);
        public static readonly GuardianSite GS039 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector RU-O d6-16", "A 2 a", (decimal?)951.1875, (decimal?)-223.25, (decimal?)-152.40625, BlueprintType.Weapon);
        public static readonly GuardianSite GS040 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector SU-O d6-54", "B 2", (decimal?)981.125, (decimal?)-202.65625, (decimal?)-110.53125, BlueprintType.None);
        public static readonly GuardianSite GS041 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector SZ-G b26-0", "B 2", (decimal?)925.625, (decimal?)-234.96875, (decimal?)-132.6875, BlueprintType.None);
        public static readonly GuardianSite GS042 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector VV-D b28-0", "A 1", (decimal?)895.71875, (decimal?)-201.75, (decimal?)-101.4375, BlueprintType.None);
        public static readonly GuardianSite GS043 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector WP-O b22-1", "A 3", (decimal?)934.125, (decimal?)-172.125, (decimal?)-205.34375, BlueprintType.None);
        public static readonly GuardianSite GS044 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector WZ-O b22-4", "C 5", (decimal?)1011.0625, (decimal?)-131.78125, (decimal?)-210.4375, BlueprintType.None);
        public static readonly GuardianSite GS045 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector XH-H c11-21", "B 1", (decimal?)894.3125, (decimal?)-153.75, (decimal?)-212.3125, BlueprintType.Weapon);
        public static readonly GuardianSite GS046 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector XL-K b25-1", "B 1", (decimal?)950.3125, (decimal?)-57.625, (decimal?)-161.40625, BlueprintType.None);
        public static readonly GuardianSite GS047 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector YF-N b23-5", "A 5", (decimal?)962.15625, (decimal?)-144.46875, (decimal?)-188.53125, BlueprintType.None);
        public static readonly GuardianSite GS048 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector YF-N b23-6", "A 3", (decimal?)962.84375, (decimal?)-132.78125, (decimal?)-194.65625, BlueprintType.None);
        public static readonly GuardianSite GS049 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector ZL-L b24-2", "B 1", (decimal?)896.78125, (decimal?)-134.90625, (decimal?)-182.09375, BlueprintType.Weapon);
        public static readonly GuardianSite GS050 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector ZL-L b24-7", "C 4", (decimal?)899, (decimal?)-131.1875, (decimal?)-173, BlueprintType.None);
        public static readonly GuardianSite GS051 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector ZS-F c12-27", "B 3", (decimal?)894.6875, (decimal?)-113.5625, (decimal?)-166.46875, BlueprintType.Weapon);
        public static readonly GuardianSite GS052 = new GuardianSite( GuardianSiteType.Structure, "Col 173 Sector ZV-M b23-1", "A 1", (decimal?)914.375, (decimal?)-167.3125, (decimal?)-202.5625, BlueprintType.None);
        public static readonly GuardianSite GS053 = new GuardianSite( GuardianSiteType.Structure, "HD 62755", "10 b", (decimal?)1001.78125, (decimal?)-201.53125, (decimal?)-167.125, BlueprintType.None);
        public static readonly GuardianSite GS054 = new GuardianSite( GuardianSiteType.Structure, "HD 63154", "B 3 a", (decimal?)979.46875, (decimal?)-207.40625, (decimal?)-131.59375, BlueprintType.Module);
        public static readonly GuardianSite GS055 = new GuardianSite( GuardianSiteType.Structure, "HIP 36781", "A 6 b", (decimal?)682.03125, (decimal?)-154.03125, (decimal?)-126.65625, BlueprintType.Vessel);
        public static readonly GuardianSite GS056 = new GuardianSite( GuardianSiteType.Structure, "HIP 39768", "A 14 f", (decimal?)866.59375, (decimal?)-119.125, (decimal?)-109.03125, BlueprintType.None);
        public static readonly GuardianSite GS057 = new GuardianSite( GuardianSiteType.Structure, "HIP 39890", "10 a", (decimal?)648.5625, (decimal?)-137.21875, (decimal?)-0.3125, BlueprintType.None);
        public static readonly GuardianSite GS058 = new GuardianSite( GuardianSiteType.Structure, "HIP 41730", "13 g a", (decimal?)818.375, (decimal?)-7.90625, (decimal?)-149.9375, BlueprintType.None);
        public static readonly GuardianSite GS059 = new GuardianSite( GuardianSiteType.Structure, "IC 2391 Sector CA-A d43", "12 a", (decimal?)591.56, (decimal?)-112.15625, (decimal?)-36.1875, BlueprintType.Vessel);
        public static readonly GuardianSite GS060 = new GuardianSite( GuardianSiteType.Structure, "IC 2391 Sector FL-X b1-7", "A 3", (decimal?)611.34375, (decimal?)-78.40625, (decimal?)-51.6875, BlueprintType.None);
        public static readonly GuardianSite GS061 = new GuardianSite( GuardianSiteType.Structure, "IC 2391 Sector HG-X b1-8", "C 2", (decimal?)603.9375, (decimal?)-104.15625, (decimal?)-61.5625, BlueprintType.Vessel);
        public static readonly GuardianSite GS062 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector DL-Y d61", "A 2", (decimal?)734.6875, (decimal?)-150.28125, (decimal?)-272.28125, BlueprintType.Module);
        public static readonly GuardianSite GS063 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector IR-W d1-77", "AB 1 a", (decimal?)742.78125, (decimal?)-156.90625, (decimal?)-260.96875, BlueprintType.Weapon);
        public static readonly GuardianSite GS064 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector JR-W c1-1", "1 c", (decimal?)739.625, (decimal?)-158.09375, (decimal?)-278.3125, BlueprintType.None);
        public static readonly GuardianSite GS065 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector MC-V c2-5", "E 1", (decimal?)795.6875, (decimal?)-114.34375, (decimal?)-258.25, BlueprintType.Weapon);
        public static readonly GuardianSite GS066 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector TO-R c4-10", "B 1", (decimal?)758.875, (decimal?)-122, (decimal?)-165.03125, BlueprintType.Vessel);
        public static readonly GuardianSite GS067 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector TY-R b4-6", "C 1", (decimal?)792.40625, (decimal?)-119.6875, (decimal?)-264.21875, BlueprintType.None);
        public static readonly GuardianSite GS068 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector UO-R c4-4", "B 5", (decimal?)800.25, (decimal?)-118.84375, (decimal?)-158.875, BlueprintType.None);
        public static readonly GuardianSite GS069 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector VJ-R c4-22", "A 1", (decimal?)746.34375, (decimal?)-153.09375, (decimal?)-164.25, BlueprintType.Vessel);
        public static readonly GuardianSite GS070 = new GuardianSite( GuardianSiteType.Structure, "NGC 2451A Sector WE-Q b5-6", "B 1", (decimal?)772.5625, (decimal?)-123.75, (decimal?)-240.125, BlueprintType.Weapon);
        public static readonly GuardianSite GS071 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector AF-A d80", "8 f", (decimal?)835.65625, (decimal?)-17.34375, (decimal?)-130.21875, BlueprintType.None);
        public static readonly GuardianSite GS072 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector BQ-X b1-4", "B 2", (decimal?)800.71875, (decimal?)26.75, (decimal?)-104.59375, BlueprintType.None);
        public static readonly GuardianSite GS073 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector CQ-Y c31", "C 1", (decimal?)800.25, (decimal?)-7.53125, (decimal?)-101.25, BlueprintType.None);
        public static readonly GuardianSite GS074 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector VY-A b8", "B 1", (decimal?)804.03125, (decimal?)5.5, (decimal?)-142.65625, BlueprintType.None);
        public static readonly GuardianSite GS075 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector XO-A c31", "B 3", (decimal?)842.3125, (decimal?)40.3125, (decimal?)-120.0625, BlueprintType.None);
        public static readonly GuardianSite GS076 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector YJ-A c12", "B 3", (decimal?)801.53125, (decimal?)-12.15625, (decimal?)-132.09375, BlueprintType.None);
        public static readonly GuardianSite GS077 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector YJ-A c33", "1", (decimal?)794.40625, (decimal?)4.75, (decimal?)-116.78125, BlueprintType.None);
        public static readonly GuardianSite GS078 = new GuardianSite( GuardianSiteType.Structure, "Pencil Sector ZJ-A c4", "B 4", (decimal?)854.90625, (decimal?)7.8125, (decimal?)-112.125, BlueprintType.None);
        public static readonly GuardianSite GS079 = new GuardianSite( GuardianSiteType.Structure, "Synuefe AH-J d10-103", "B 1", (decimal?)777.53125, (decimal?)-158.28125, (decimal?)-117.4375, BlueprintType.Vessel);
        public static readonly GuardianSite GS080 = new GuardianSite( GuardianSiteType.Structure, "Synuefe AH-J d10-20", "A 3", (decimal?)759.21875, (decimal?)-148.3125, (decimal?)-145.75, BlueprintType.Vessel);
        public static readonly GuardianSite GS081 = new GuardianSite( GuardianSiteType.Structure, "Synuefe AH-J d10-46", "C 1", (decimal?)741.3125, (decimal?)-164.8125, (decimal?)-138.78125, BlueprintType.Vessel);
        public static readonly GuardianSite GS082 = new GuardianSite( GuardianSiteType.Structure, "Synuefe BE-R c21-35", "E 2", (decimal?)782.6875, (decimal?)-96.15625, (decimal?)-135.03125, BlueprintType.Vessel);
        public static readonly GuardianSite GS083 = new GuardianSite( GuardianSiteType.Structure, "Synuefe BH-J d10-68", "C 1", (decimal?)831.375, (decimal?)-149.125, (decimal?)-175.96875, BlueprintType.Weapon);
        public static readonly GuardianSite GS084 = new GuardianSite( GuardianSiteType.Structure, "Synuefe BZ-Q c21-11", "B 5", (decimal?)727.21875, (decimal?)-119.90625, (decimal?)-128.15625, BlueprintType.Vessel);
        public static readonly GuardianSite GS085 = new GuardianSite( GuardianSiteType.Structure, "Synuefe CE-R c21-6", "C 1", (decimal?)828.1875, (decimal?)-78, (decimal?)-105.1875, BlueprintType.None);
        public static readonly GuardianSite GS086 = new GuardianSite( GuardianSiteType.Structure, "Synuefe DJ-G b44-3", "A 5", (decimal?)679.59375, (decimal?)-105.6875, (decimal?)-122.40625, BlueprintType.Vessel);
        public static readonly GuardianSite GS087 = new GuardianSite( GuardianSiteType.Structure, "Synuefe DK-D b46-4", "C 1", (decimal?)652.9375, (decimal?)-63.90625, (decimal?)-80.78125, BlueprintType.None);
        public static readonly GuardianSite GS088 = new GuardianSite( GuardianSiteType.Structure, "Synuefe DZ-Q c21-14", "10 a", (decimal?)793.96875, (decimal?)-122.46875, (decimal?)-135.03125, BlueprintType.Vessel);
        public static readonly GuardianSite GS089 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EA-U b50-3", "B 6", (decimal?)673.78125, (decimal?)-106.625, (decimal?)25.75, BlueprintType.None);
        public static readonly GuardianSite GS090 = new GuardianSite( GuardianSiteType.Structure, "Synuefe ED-I b43-8", "A 2", (decimal?)791.1875, (decimal?)-120.625, (decimal?)-129.40625, BlueprintType.Vessel);
        public static readonly GuardianSite GS091 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EN-H d11-106", "6 a", (decimal?)785.15625, (decimal?)-167.0625, (decimal?)-75.46875, BlueprintType.Weapon);
        public static readonly GuardianSite GS092 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EN-H d11-28", "8 b", (decimal?)771.78125, (decimal?)-145, (decimal?)-26.65625, BlueprintType.Vessel);
        public static readonly GuardianSite GS093 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EN-H d11-29", "6 b", (decimal?)745.6875, (decimal?)-115, (decimal?)-91.34375, BlueprintType.Vessel);
        public static readonly GuardianSite GS094 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EN-H d11-5", "4 e", (decimal?)813.625, (decimal?)-121.28125, (decimal?)-90.25, BlueprintType.None);
        public static readonly GuardianSite GS095 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EN-H d11-96", "7 a", (decimal?)757.125, (decimal?)-179.3125, (decimal?)-96.0625, BlueprintType.Vessel);
        public static readonly GuardianSite GS096 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EU-Q c21-10", "A 3", (decimal?)758.65625, (decimal?)-176.90625, (decimal?)-133.21875, BlueprintType.Weapon);
        public static readonly GuardianSite GS097 = new GuardianSite( GuardianSiteType.Structure, "Synuefe EU-Q c21-15", "A 1", (decimal?)754.15625, (decimal?)-171.84375, (decimal?)-138.09375, BlueprintType.Vessel);
        public static readonly GuardianSite GS098 = new GuardianSite( GuardianSiteType.Structure, "Synuefe FK-P c22-2", "E 1", (decimal?)809.75, (decimal?)-104.96875, (decimal?)-104.5625, BlueprintType.Weapon);
        public static readonly GuardianSite GS099 = new GuardianSite( GuardianSiteType.Structure, "Synuefe GQ-N c23-21", "B 3", (decimal?)655.53125, (decimal?)-66.34375, (decimal?)-62.1875, BlueprintType.Vessel);
        public static readonly GuardianSite GS100 = new GuardianSite( GuardianSiteType.Structure, "Synuefe GT-H b43-1", "C 4", (decimal?)749, (decimal?)-163.09375, (decimal?)-128.0625, BlueprintType.Module);
        public static readonly GuardianSite GS101 = new GuardianSite( GuardianSiteType.Structure, "Synuefe GV-T b50-4", "B 1", (decimal?)663.46875, (decimal?)-127.5625, (decimal?)27.25, BlueprintType.Weapon);
        public static readonly GuardianSite GS102 = new GuardianSite( GuardianSiteType.Structure, "Synuefe HE-G b44-5", "B 2", (decimal?)726.6875, (decimal?)-128.9375, (decimal?)-112.71875, BlueprintType.Vessel);
        public static readonly GuardianSite GS103 = new GuardianSite( GuardianSiteType.Structure, "Synuefe HF-P c22-17", "B 1", (decimal?)783.84375, (decimal?)-114.40625, (decimal?)-92.53125, BlueprintType.Weapon);
        public static readonly GuardianSite GS104 = new GuardianSite( GuardianSiteType.Structure, "Synuefe HF-V b49-5", "C 1", (decimal?)692.46875, (decimal?)-181.15625, (decimal?)6.0625, BlueprintType.None);
        public static readonly GuardianSite GS105 = new GuardianSite( GuardianSiteType.Structure, "Synuefe HP-E b45-8", "D 3", (decimal?)688.59375, (decimal?)-120.40625, (decimal?)-87.21875, BlueprintType.Vessel);
        public static readonly GuardianSite GS106 = new GuardianSite( GuardianSiteType.Structure, "Synuefe HT-F d12-29", "C 3", (decimal?)665.78125, (decimal?)-131.96875, (decimal?)38.65625, BlueprintType.None);
        public static readonly GuardianSite GS107 = new GuardianSite( GuardianSiteType.Structure, "Synuefe IE-G b44-2", "A 4", (decimal?)737.25, (decimal?)-130.28125, (decimal?)-111.34375, BlueprintType.Vessel);
        public static readonly GuardianSite GS108 = new GuardianSite( GuardianSiteType.Structure, "Synuefe IL-N c23-19", "B 2", (decimal?)667.40625, (decimal?)-120.5625, (decimal?)-58.8125, BlueprintType.Vessel);
        public static readonly GuardianSite GS109 = new GuardianSite( GuardianSiteType.Structure, "Synuefe JP-E b45-4", "C 1", (decimal?)724.34375, (decimal?)-111.09375, (decimal?)-103.5625, BlueprintType.Vessel);
        public static readonly GuardianSite GS110 = new GuardianSite( GuardianSiteType.Structure, "Synuefe KZ-F b44-5", "A 1", (decimal?)747.90625, (decimal?)-161.21875, (decimal?)-109.46875, BlueprintType.Vessel);
        public static readonly GuardianSite GS111 = new GuardianSite( GuardianSiteType.Structure, "Synuefe LQ-T b50-1", "B 2", (decimal?)728.5, (decimal?)-155.65625, (decimal?)19.3125, BlueprintType.Module);
        public static readonly GuardianSite GS112 = new GuardianSite( GuardianSiteType.Structure, "Synuefe LY-I b42-2", "C 2", (decimal?)814.71875, (decimal?)-222.78125, (decimal?)-151.15625, BlueprintType.None);
        public static readonly GuardianSite GS113 = new GuardianSite( GuardianSiteType.Structure, "Synuefe NB-B b47-10", "B 2", (decimal?)643.4375, (decimal?)-113.84375, (decimal?)-61.5625, BlueprintType.Vessel);
        public static readonly GuardianSite GS114 = new GuardianSite( GuardianSiteType.Structure, "Synuefe NL-N c23-4", "B 3", (decimal?)860.125, (decimal?)-124.59375, (decimal?)-61.0625, BlueprintType.None);
        public static readonly GuardianSite GS115 = new GuardianSite( GuardianSiteType.Structure, "Synuefe NU-F b44-5", "B 1", (decimal?)768.21875, (decimal?)-166.0625, (decimal?)-106.40625, BlueprintType.Vessel);
        public static readonly GuardianSite GS116 = new GuardianSite( GuardianSiteType.Structure, "Synuefe OT-I b42-4", "B 2", (decimal?)817.5, (decimal?)-238.875, (decimal?)-147.40625, BlueprintType.None);
        public static readonly GuardianSite GS117 = new GuardianSite( GuardianSiteType.Structure, "Synuefe PF-E b45-5", "B 2", (decimal?)758.5, (decimal?)-164.65625, (decimal?)-101.28125, BlueprintType.Vessel);
        public static readonly GuardianSite GS118 = new GuardianSite( GuardianSiteType.Structure, "Synuefe PM-L c24-24", "C 6", (decimal?)706.6875, (decimal?)-158.875, (decimal?)10.5, BlueprintType.Weapon);
        public static readonly GuardianSite GS119 = new GuardianSite( GuardianSiteType.Structure, "Synuefe PX-J c25-8", "7 a", (decimal?)649.6875, (decimal?)-124.25, (decimal?)32.4375, BlueprintType.Module);
        public static readonly GuardianSite GS120 = new GuardianSite( GuardianSiteType.Structure, "Synuefe SP-F b44-0", "C 1", (decimal?)831.625, (decimal?)-187.5625, (decimal?)-121.84375, BlueprintType.Weapon);
        public static readonly GuardianSite GS121 = new GuardianSite( GuardianSiteType.Structure, "Synuefe TP-F b44-0", "CD 1", (decimal?)838.75, (decimal?)-197.84375, (decimal?)-111.84375, BlueprintType.None);
        public static readonly GuardianSite GS122 = new GuardianSite( GuardianSiteType.Structure, "Synuefe VK-F b44-0", "B 1", (decimal?)836.9375, (decimal?)-220.78125, (decimal?)-122.75, BlueprintType.Weapon);
        public static readonly GuardianSite GS123 = new GuardianSite( GuardianSiteType.Structure, "Synuefe ZG-J d10-79", "B 1", (decimal?)728, (decimal?)-121.34375, (decimal?)-133.25, BlueprintType.Vessel);
        public static readonly GuardianSite GS124 = new GuardianSite( GuardianSiteType.Structure, "Synuefe ZL-J d10-109", "E 3", (decimal?)852.65625, (decimal?)-51.125, (decimal?)-124.84375, BlueprintType.None);
        public static readonly GuardianSite GS125 = new GuardianSite( GuardianSiteType.Structure, "Synuefe ZL-J d10-119", "9 b", (decimal?)834.21875, (decimal?)-51.21875, (decimal?)-154.65625, BlueprintType.None);
        public static readonly GuardianSite GS126 = new GuardianSite( GuardianSiteType.Structure, "Synuefe ZR-I b43-10", "D 2", (decimal?)811.40625, (decimal?)-60.4375, (decimal?)-144.71875, BlueprintType.None);
        public static readonly GuardianSite GS127 = new GuardianSite( GuardianSiteType.Structure, "Trapezium Sector YU-X c1-2", "1 a", (decimal?)573.59375, (decimal?)-339.46875, (decimal?)-1167.65625, BlueprintType.None);
        public static readonly GuardianSite GS128 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region BQ-Y d110", "4 b", (decimal?)953.375, (decimal?)-93.8125, (decimal?)-76.28125, BlueprintType.None);
        public static readonly GuardianSite GS129 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region BQ-Y d94", "9 b", (decimal?)916.6875, (decimal?)-99.46875, (decimal?)-86.5625, BlueprintType.None);
        public static readonly GuardianSite GS130 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region CQ-Y c0", "A 2", (decimal?)940.78125, (decimal?)-122.34375, (decimal?)-133.0625, BlueprintType.Weapon);
        public static readonly GuardianSite GS131 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region DL-Y d112", "1 a", (decimal?)924.46875, (decimal?)-171.8125, (decimal?)-98.21875, BlueprintType.Module);
        public static readonly GuardianSite GS132 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region DL-Y d83", "5 b", (decimal?)930.4375, (decimal?)-165.78125, (decimal?)-65.1875, BlueprintType.Module);
        public static readonly GuardianSite GS133 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region DL-Y d91", "5 a", (decimal?)922.78125, (decimal?)-126, (decimal?)-101.78125, BlueprintType.Weapon);
        public static readonly GuardianSite GS134 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region EG-X b1-1", "A 1", (decimal?)957.6875, (decimal?)-143.34375, (decimal?)-123.40625, BlueprintType.Weapon);
        public static readonly GuardianSite GS135 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region EL-Y d32", "B 1", (decimal?)1000.65625, (decimal?)-166.21875, (decimal?)-64.15625, BlueprintType.None);
        public static readonly GuardianSite GS136 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region EL-Y d55", "1", (decimal?)975.78125, (decimal?)-156.8125, (decimal?)-75.34375, BlueprintType.Weapon);
        public static readonly GuardianSite GS137 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region FL-Y d63", "B 1", (decimal?)1064.5, (decimal?)-144.03125, (decimal?)-101.71875, BlueprintType.None);
        public static readonly GuardianSite GS138 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region HB-X c1-28", "E 2", (decimal?)1073.0625, (decimal?)-100.65625, (decimal?)-92.75, BlueprintType.None);
        public static readonly GuardianSite GS139 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region IW-W b1-3", "B 1", (decimal?)963.03125, (decimal?)-165.1875, (decimal?)-124.4375, BlueprintType.None);
        public static readonly GuardianSite GS140 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region JS-T b3-0", "A 3", (decimal?)899.9375, (decimal?)-142.1875, (decimal?)-82.25, BlueprintType.Weapon);
        public static readonly GuardianSite GS141 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region KR-W c1-24", "7 a", (decimal?)1036.875, (decimal?)-163.59375, (decimal?)-85.96875, BlueprintType.None);
        public static readonly GuardianSite GS142 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region PY-R b4-3", "B 2", (decimal?)935.59375, (decimal?)-138.1875, (decimal?)-48.40625, BlueprintType.None);
        public static readonly GuardianSite GS143 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region QI-T b3-2", "B 1", (decimal?)967.28125, (decimal?)-180.3125, (decimal?)-72.84375, BlueprintType.None);
        public static readonly GuardianSite GS144 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region RY-R b4-7", "A 1", (decimal?)977.90625, (decimal?)-126.21875, (decimal?)-57.65625, BlueprintType.None);
        public static readonly GuardianSite GS145 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region TD-S b4-5", "C 1", (decimal?)1071.21875, (decimal?)-121.03125, (decimal?)-50.09375, BlueprintType.None);
        public static readonly GuardianSite GS146 = new GuardianSite( GuardianSiteType.Structure, "Vela Dark Region ZE-A d66", "6 b", (decimal?)966.90625, (decimal?)-158, (decimal?)-131.6875, BlueprintType.Weapon);
        public static readonly GuardianSite GS147 = new GuardianSite( GuardianSiteType.Structure, "Wregoe BU-Y b2-0", "1 c", (decimal?)1077.375, (decimal?)400.5625, (decimal?)-993.375, BlueprintType.None);
        public static readonly GuardianSite GS148 = new GuardianSite( GuardianSiteType.Structure, "Wregoe BU-Y b2-0", "1 c", (decimal?)1077.375, (decimal?)400.5625, (decimal?)-993.375, BlueprintType.None);
        public static readonly GuardianSite GS149 = new GuardianSite( GuardianSiteType.Structure, "Wregoe CQ-G d10-153", "C 2", (decimal?)794.53125, (decimal?)26.5, (decimal?)-175.90625, BlueprintType.None);
        public static readonly GuardianSite GS150 = new GuardianSite( GuardianSiteType.Structure, "Wregoe CQ-G d10-56", "6 a", (decimal?)798.8125, (decimal?)49.90625, (decimal?)-153.625, BlueprintType.None);
        public static readonly GuardianSite GS151 = new GuardianSite( GuardianSiteType.Structure, "Wregoe DQ-G d10-42", "5 a", (decimal?)865.0625, (decimal?)11.625, (decimal?)-136.4375, BlueprintType.None);
        public static readonly GuardianSite GS152 = new GuardianSite( GuardianSiteType.Structure, "Wregoe IM-Z b41-2", "B 2", (decimal?)820.71875, (decimal?)55.78125, (decimal?)-164.09375, BlueprintType.None);
        public static readonly GuardianSite GS153 = new GuardianSite( GuardianSiteType.Structure, "Wregoe JR-A b41-3", "F 2", (decimal?)801.34375, (decimal?)-3.0625, (decimal?)-166.46875, BlueprintType.None);
        public static readonly GuardianSite GS154 = new GuardianSite( GuardianSiteType.Structure, "Wregoe JR-L c21-1", "C 2", (decimal?)855.25, (decimal?)17.34375, (decimal?)-137.90625, BlueprintType.None);
        public static readonly GuardianSite GS155 = new GuardianSite( GuardianSiteType.Structure, "Wregoe KH-Z b41-0", "C 3", (decimal?)826, (decimal?)51.375, (decimal?)-162.6875, BlueprintType.None);





        public GuardianSiteType type { get; set; }
        public string systemName { get; set; }
        public string body { get; set; }
        public decimal? x;                   // x coordinate of system
        public decimal? y;                   // y coordinate of system
        public decimal? z;                   // z coordinate of system
        public BlueprintType blueprintType { get; set; }

        // Calucated distance from target system ( Gets set and returned with TryGetNearestNebula )
        [PublicAPI("The calculated distance to the site from the current system.")]
        public decimal? distance { get; set; }

        // dummy used to ensure that the static constructor has run
        public GuardianSite ()
        { }

        private GuardianSite ( GuardianSiteType type,
                               string systemName,
                               string body,
                               decimal? x,
                               decimal? y,
                               decimal? z,
                               BlueprintType blueprintType )
        {
            this.type = type;
            this.systemName = systemName;
            this.body = body;
            this.x = x;
            this.y = y;
            this.z = z;
            this.blueprintType = blueprintType;

            AllOfThem.Add( this );
        }

        public static GuardianSite TryGetNearestGuardianSite ( decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in AllOfThem )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            GuardianSite closest = listGuardianSites.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );

            return closest;
        }

        public static List<GuardianSite> TryGetNearestGuardianSites ( decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000 )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in AllOfThem )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<GuardianSite> closestList = listGuardianSites.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            //foreach( var guardianSite in closestList ) {
            //    guardianSite.distance = Functions.StellarDistanceLy( guardianSite.distance );
            //}
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }

            return closestList;
        }

        public static GuardianSite TryGetNearestGuardianSite ( GuardianSiteType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in AllOfThem.Where( x=> x.type == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            GuardianSite closest = listGuardianSites.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );

            return closest;
        }

        public static List<GuardianSite> TryGetNearestGuardianSites ( GuardianSiteType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000 )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in AllOfThem.Where( x=> x.type == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<GuardianSite> closestList = listGuardianSites.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            //foreach( var guardianSite in closestList ) {
            //    guardianSite.distance = Functions.StellarDistanceLy( guardianSite.distance );
            //}
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }

            return closestList;
        }

        public static GuardianSite TryGetNearestGuardianSite ( BlueprintType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in AllOfThem.Where( x=> x.blueprintType == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            GuardianSite closest = listGuardianSites.OrderBy( s => s.distance).First();
            closest.distance = Functions.StellarDistanceLy( closest.distance );

            return closest;
        }

        public static List<GuardianSite> TryGetNearestGuardianSites ( BlueprintType typeFilter, decimal? systemX, decimal? systemY, decimal? systemZ, int maxCount=50, int maxDistance=10000 )
        {
            List<GuardianSite> listGuardianSites = new List<GuardianSite>();

            // Get the distance (squared) of all Nebula
            foreach ( var guardianSite in AllOfThem.Where( x=> x.blueprintType == typeFilter ) )
            {
                if ( guardianSite.x != null && guardianSite.y != null && guardianSite.z != null )
                {
                    // We don't need the exact distance, use the faster method for sorting purposes
                    guardianSite.distance = Functions.StellarDistanceSquare( systemX, systemY, systemZ, guardianSite.x, guardianSite.y, guardianSite.z );
                    listGuardianSites.Add( guardianSite );
                }
            }

            var maxDistanceSquared = maxDistance*maxDistance;
            List<GuardianSite> closestList = listGuardianSites.Where( s => s.distance <= maxDistanceSquared ).OrderBy( s => s.distance).Take(maxCount).ToList();
            //foreach( var guardianSite in closestList ) {
            //    guardianSite.distance = Functions.StellarDistanceLy( guardianSite.distance );
            //}
            for(int i = 0; i< closestList.Count; i++) {
                closestList[i].distance = Functions.StellarDistanceLy( closestList[i].distance );
            }

            return closestList;
        }

        public static GuardianSite TryGetNearestNebula ( StarSystem starsystem )
        {
            return TryGetNearestGuardianSite( starsystem.x, starsystem.y, starsystem.z );
        }
    }
}
