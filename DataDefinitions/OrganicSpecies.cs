using System.Collections.Generic;
using System.Linq;
using Utilities;
using Newtonsoft.Json;

namespace EddiDataDefinitions
{
    public class OrganicSpecies : ResourceBasedLocalizedEDName<OrganicSpecies>
    {
        static OrganicSpecies ()
        {
            resourceManager = Properties.OrganicSpecies.ResourceManager;
            resourceManager.IgnoreCase = true;
            missingEDNameHandler = ( edname ) => new OrganicSpecies( NormalizeSpecies( edname ) );
        }

        // Terrestrial Species
        // - Many of these ednames have been generated and assumed from the variant name and may not be correct
        public static readonly OrganicSpecies AleoidaArcus = new OrganicSpecies( "Aleoids_01", OrganicGenus.Aleoids, 7252500,(decimal?)0.3, (decimal?)175, (decimal?)180, "","CarbonDioxide","None","B;A;F;K;M;L;T;TTS;Y;N" );
        public static readonly OrganicSpecies AleoidaCoronamus = new OrganicSpecies( "Aleoids_02", OrganicGenus.Aleoids, 6284600,(decimal?)0.3, (decimal?)180, (decimal?)190, "","CarbonDioxide","None","B;A;F;K;M;L;T;TTS;Y;N" );
        public static readonly OrganicSpecies AleoidaGravis = new OrganicSpecies( "Aleoids_05", OrganicGenus.Aleoids, 12934900,(decimal?)0.3, (decimal?)190, (decimal?)195, "","CarbonDioxide","None","B;A;F;K;M;L;T;TTS;Y;N" );
        public static readonly OrganicSpecies AleoidaLaminiae = new OrganicSpecies( "Aleoids_04", OrganicGenus.Aleoids, 3385200,(decimal?)0.3, null, null, "","Ammonia","","B;A;F;K;M;L;T;TTS;Y;N" );
        public static readonly OrganicSpecies AleoidaSpica = new OrganicSpecies( "Aleoids_03", OrganicGenus.Aleoids, 3385200,(decimal?)0.3, null, null, "","Ammonia","","B;A;F;K;M;L;T;TTS;Y;N" );
        public static readonly OrganicSpecies AmphoraPlant = new OrganicSpecies( "Vents", OrganicGenus.Vents, 1628800,null, (decimal?)1000, null, "MetalRichBody","None","","A" );
        public static readonly OrganicSpecies BlatteumBioluminescentAnemone = new OrganicSpecies( "SphereEFGH", OrganicGenus.Sphere, 1499900,null, (decimal?)210, null, "MetalRichBody;HighMetalContentBody","Argon;CarbonDioxide;CarbonDioxideRich;HotSilicateVapour;None","","B" );
        public static readonly OrganicSpecies CroceumAnemone = new OrganicSpecies( "SphereABCD_01", OrganicGenus.Sphere, 1499900,(decimal?)0.42, (decimal?)200, (decimal?)440, "RockyBody","Water;SulphurDioxide;None","","B;A" );
        public static readonly OrganicSpecies LuteolumAnemone = new OrganicSpecies( "Sphere", OrganicGenus.Sphere, 1499900,(decimal?)1.32, (decimal?)200, (decimal?)440, "RockyBody","CarbonDioxide;Water;SulphurDioxide;None","","B" );
        public static readonly OrganicSpecies PrasinumBioluminescentAnemone = new OrganicSpecies( "SphereEFGH_02", OrganicGenus.Sphere, 1499900,null, (decimal?)20, null, "RockyBody;MetalRichBody;HighMetalContentBody","CarbonDioxide;Argon;Ammonia;Nitrogen;SulphurDioxide;NeonRich;HotSulphurDioxide;None","","O" );
        public static readonly OrganicSpecies PuniceumAnemone = new OrganicSpecies( "SphereABCD_02", OrganicGenus.Sphere, 1499900,(decimal?)2.61, (decimal?)65, (decimal?)860, "IceBody","Oxygen;Nitrogen;None","","O;W" );
        public static readonly OrganicSpecies RoseumAnemone = new OrganicSpecies( "SphereABCD_03", OrganicGenus.Sphere, 1499900,(decimal?)0.45, (decimal?)200, (decimal?)440, "RockyBody","SulphurDioxide;None","","B" );
        public static readonly OrganicSpecies RoseumBioluminescentAnemone = new OrganicSpecies( "SphereEFGH_03", OrganicGenus.Sphere, 1499900,null, (decimal?)190, null, "MetalRichBody;HighMetalContentBody","CarbonDioxide;SulphurDioxide;None","","B" );
        public static readonly OrganicSpecies RubeumBioluminescentAnemone = new OrganicSpecies( "SphereEFGH_01", OrganicGenus.Sphere, 1499900,null, (decimal?)160, null, "MetalRichBody;HighMetalContentBody","Argon;CarbonDioxide;SulphurDioxide;None","","B" );
        public static readonly OrganicSpecies BacteriumAcies = new OrganicSpecies( "Bacterial_04", OrganicGenus.Bacterial, 1000000,(decimal?)0.75, null, null, "IcyBody;RockyIceBody","Neon;NeonRich","","" );
        public static readonly OrganicSpecies BacteriumAlcyoneum = new OrganicSpecies( "Bacterial_06", OrganicGenus.Bacterial, 1658500,(decimal?)0.38, null, null, "RockyBody;HighMetalContentBody;RockyIceBody;IcyBody","Ammonia","","" );
        public static readonly OrganicSpecies BacteriumAurasus = new OrganicSpecies( "Bacterial_01", OrganicGenus.Bacterial, 1000000,(decimal?)1, null, null, "","CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies BacteriumBullaris = new OrganicSpecies( "Bacterial_10", OrganicGenus.Bacterial, 1152500,(decimal?)0.61, null, null, "RockyBody;HighMetalContentBody;RockyIceBody;IcyBody","Methane;MethaneRich","","" );
        public static readonly OrganicSpecies BacteriumCerbrus = new OrganicSpecies( "Bacterial_12", OrganicGenus.Bacterial, 1689800,(decimal?)1, null, null, "","Water;WaterRich;SulphurDioxide","","" );
        public static readonly OrganicSpecies BacteriumInformem = new OrganicSpecies( "Bacterial_08", OrganicGenus.Bacterial, 8418000,(decimal?)0.6, null, null, "RockyBody;HighMetalContentBody;RockyIceBody;IcyBody","Nitrogen","","" );
        public static readonly OrganicSpecies BacteriumNebulus = new OrganicSpecies( "Bacterial_02", OrganicGenus.Bacterial, 5289900,(decimal?)0.55, null, null, "IcyBody","Helium","","" );
        public static readonly OrganicSpecies BacteriumOmentum = new OrganicSpecies( "Bacterial_11", OrganicGenus.Bacterial, 4638900,(decimal?)0.61, null, null, "IcyBody","Neon;NeonRich","Nitrogen;Ammonia","" );
        public static readonly OrganicSpecies BacteriumScopulum = new OrganicSpecies( "Bacterial_03", OrganicGenus.Bacterial, 4934500,(decimal?)0.62, null, null, "IcyBody;RockyIceBody","Neon;NeonRich","Carbon;Methane","" );
        public static readonly OrganicSpecies BacteriumTela = new OrganicSpecies( "Bacterial_07", OrganicGenus.Bacterial, 1949000,(decimal?)0.62, null, null, "RockyBody;HighMetalContentBody;RockyIceBody;IcyBody","Any","Helium;Iron;Silicate","" );
        public static readonly OrganicSpecies BacteriumVerrata = new OrganicSpecies( "Bacterial_13", OrganicGenus.Bacterial, 3897000,(decimal?)0.61, null, null, "IcyBody;RockyBody;RockyIceBody","Neon;NeonRich","Water","" );
        public static readonly OrganicSpecies BacteriumVesicula = new OrganicSpecies( "Bacterial_05", OrganicGenus.Bacterial, 1000000,(decimal?)1, null, null, "IcyBody;RockyBody;HighMetalContentBody;RockyIceBody","Argon;ArgonRich","","" );
        public static readonly OrganicSpecies BacteriumVolu = new OrganicSpecies( "Bacterial_09", OrganicGenus.Bacterial, 7774700,(decimal?)0.61, null, null, "IcyBody;RockyBody;HighMetalContentBody;RockyIceBody","Oxygen","","" );
        public static readonly OrganicSpecies BarkMounds = new OrganicSpecies( "Cone", OrganicGenus.Cone, 1471900,null, (decimal?)88, (decimal?)440, "RockyBody;HighMetalContentBody;RockyIceBody;IcyBody","None;CarbonDioxide;CarbonDioxideRich;ArgonRich;SulphurDioxide;ThickArgonRich","","" );
        public static readonly OrganicSpecies AureumBrainTree = new OrganicSpecies( "SeedEFGH_01", OrganicGenus.Brancae, 1593700,null, (decimal?)300, (decimal?)500, "MetalRichBody;HighMetalContentBody","None;SulphurDioxide","Any","" );
        public static readonly OrganicSpecies GypseeumBrainTree = new OrganicSpecies( "SeedABCD_01", OrganicGenus.Brancae, 1593700,(decimal?)0.42, (decimal?)170, (decimal?)330, "RockyBody","Ammonia;None;Oxygen;SulphurDioxide","Any","" );
        public static readonly OrganicSpecies LindigoticumBrainTree = new OrganicSpecies( "SeedEFGH_03", OrganicGenus.Brancae, 1593700,null, (decimal?)300, (decimal?)500, "RockyBody;HighMetalContentBody","None","Any","" );
        public static readonly OrganicSpecies LividumBrainTree = new OrganicSpecies( "SeedEFGH", OrganicGenus.Brancae, 1593700,(decimal?)0.48, (decimal?)300, (decimal?)500, "RockyBody","None;Water;SulphurDioxide","Any","" );
        public static readonly OrganicSpecies OstrinumBrainTree = new OrganicSpecies( "SeedABCD_02", OrganicGenus.Brancae, 1593700,null, (decimal?)20, null, "MetalRichBody;HighMetalContentBody","None;CarbonDioxide;Ammonia;CarbonDioxideRich;ArgonRich;SulphurDioxide;Helium;NeonRich","Any","" );
        public static readonly OrganicSpecies PuniceumBrainTree = new OrganicSpecies( "SeedEFGH_02", OrganicGenus.Brancae, 1593700,null, (decimal?)20, null, "MetalRichBody;HighMetalContentBody","None;CarbonDioxide;Oxygen;SulphurDioxide;Helium;NeonRich","Any","" );
        public static readonly OrganicSpecies RoseumBrainTree = new OrganicSpecies( "Seed", OrganicGenus.Brancae, 1593700,null, (decimal?)115, (decimal?)500, "RockyBody;MetalRichBody;HighMetalContentBody;RockyIceBody","None;CarbonDioxide;Argon;Ammonia;CarbonDioxideRich;Oxygen;Water;SulphurDioxide;ArgonRich;WaterRich","Any","" );
        public static readonly OrganicSpecies VirideBrainTree = new OrganicSpecies( "SeedABCD_03", OrganicGenus.Brancae, 1593700,(decimal?)0.4, (decimal?)100, (decimal?)255, "RockyIceBody","Ammonia;None;SulphurDioxide","Any","" );
        public static readonly OrganicSpecies CactoidaCortexum = new OrganicSpecies( "Cactoid_01", OrganicGenus.Cactoid, 3667600,(decimal?)0.27, (decimal?)158, (decimal?)196, "RockyBody;HighMetalContentBody","CarbonDioxide","None","F;G;A;L;K;N;B;M;H" );
        public static readonly OrganicSpecies CactoidaLapis = new OrganicSpecies( "Cactoid_02", OrganicGenus.Cactoid, 2483600,(decimal?)0.28, (decimal?)160, (decimal?)225, "RockyBody;HighMetalContentBody","Ammonia","","F;G;H;A;K;N;B;A" );
        public static readonly OrganicSpecies CactoidaPeperatis = new OrganicSpecies( "Cactoid_05", OrganicGenus.Cactoid, 2483600,(decimal?)0.28, (decimal?)160, (decimal?)186, "RockyBody;HighMetalContentBody","Ammonia","","F;G;A;K;N;B;H" );
        public static readonly OrganicSpecies CactoidaPullulanta = new OrganicSpecies( "Cactoid_04", OrganicGenus.Cactoid, 3667600,(decimal?)0.27, (decimal?)127, (decimal?)195, "RockyBody;HighMetalContentBody","CarbonDioxide","None","F;G;H;A;K;N;B" );
        public static readonly OrganicSpecies CactoidaVermis = new OrganicSpecies( "Cactoid_03", OrganicGenus.Cactoid, 16202800,(decimal?)0.28, (decimal?)160, (decimal?)450, "RockyBody;HighMetalContentBody","Water;SulphurDioxide","","F;G;H;A;M;N;B;K" );
        public static readonly OrganicSpecies ClypeusLacrimam = new OrganicSpecies( "Clypeus_01", OrganicGenus.Clypeus, 8418000,(decimal?)0.28, (decimal?)190, null, "RockyBody;HighMetalContentBody","Water;CarbonDioxide","","A;F;G;K;M;L;N" );
        public static readonly OrganicSpecies ClypeusMargaritus = new OrganicSpecies( "Clypeus_02", OrganicGenus.Clypeus, 11873200,(decimal?)0.28, (decimal?)190, null, "RockyBody;HighMetalContentBody","Water;CarbonDioxide","None","A;F;G;K;M;L;N" );
        public static readonly OrganicSpecies ClypeusSpeculumi = new OrganicSpecies( "Clypeus_03", OrganicGenus.Clypeus, 16202800,(decimal?)0.28, (decimal?)190, null, "RockyBody;HighMetalContentBody","Water;CarbonDioxide","","A;F;G;K;M;L;N" );
        public static readonly OrganicSpecies ConchaAureolas = new OrganicSpecies( "Conchas_02", OrganicGenus.Conchas, 7774700,(decimal?)0.28, null, null, "","Ammonia","","" );
        public static readonly OrganicSpecies ConchaBiconcavis = new OrganicSpecies( "Conchas_04", OrganicGenus.Conchas, 19010800,(decimal?)0.28, null, null, "","Nitrogen","None","" );
        public static readonly OrganicSpecies ConchaLabiata = new OrganicSpecies( "Conchas_03", OrganicGenus.Conchas, 2352400,(decimal?)0.28, null, (decimal?)190, "","CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies ConchaRenibus = new OrganicSpecies( "Conchas_01", OrganicGenus.Conchas, 4572400,(decimal?)0.28, (decimal?)180, (decimal?)195, "","Water;WaterRich","","" );
        public static readonly OrganicSpecies CrystallineShards = new OrganicSpecies( "Ground_Struct_Ice", OrganicGenus.Ground_Struct_Ice, 1628800,(decimal?)2, null, (decimal?)266, "IcyBody;HighMetalContentBody;RockyIceBody;RockyBody","None;CarbonDioxide;Argon;CarbonDioxideRich;Methane;ArgonRich;Neon;Helium;NeonRich","","A;F;G;K;M;S" );
        public static readonly OrganicSpecies ElectricaePluma = new OrganicSpecies( "Electricae_01", OrganicGenus.Electricae, 6284600,(decimal?)0.28, null, (decimal?)150, "IcyBody","Neon;NeonRich;Argon;ArgonRich","","A;N" );
        public static readonly OrganicSpecies ElectricaeRadialem = new OrganicSpecies( "Electricae_02", OrganicGenus.Electricae, 6284600,(decimal?)0.28, null, (decimal?)150, "IcyBody","Neon;NeonRich;Argon;ArgonRich;Methane","","" );
        public static readonly OrganicSpecies FonticuluaCampestris = new OrganicSpecies( "Fonticulus_02", OrganicGenus.Fonticulus, 1000000,(decimal?)0.28, null, (decimal?)150, "IcyBody;RockyBody","Argon","","B;A;F;G;K;M;L;T;TTS;Y;D;N;AEBE" );
        public static readonly OrganicSpecies FonticuluaDigitos = new OrganicSpecies( "Fonticulus_06", OrganicGenus.Fonticulus, 1804100,(decimal?)0.28, null, null, "IcyBody;RockyBody","Methane;MethaneRich","","B;A;F;G;K;M;L;T;TTS;Y;D;N;AEBE" );
        public static readonly OrganicSpecies FonticuluaFluctus = new OrganicSpecies( "Fonticulus_05", OrganicGenus.Fonticulus, 20000000,(decimal?)0.28, null, null, "IcyBody;RockyBody","Oxygen","","B;A;F;G;K;M;L;T;TTS;Y;D;N;AEBE" );
        public static readonly OrganicSpecies FonticuluaLapida = new OrganicSpecies( "Fonticulus_04", OrganicGenus.Fonticulus, 3111000,(decimal?)0.28, null, null, "IcyBody;RockyBody","Nitrogen","","B;A;F;G;K;M;L;T;TTS;Y;D;N;AEBE" );
        public static readonly OrganicSpecies FonticuluaSegmentatus = new OrganicSpecies( "Fonticulus_01", OrganicGenus.Fonticulus, 19010800,(decimal?)0.28, null, null, "IcyBody;RockyBody","Neon;NeonRich","None","B;A;F;G;K;M;L;T;TTS;Y;D;N;AEBE" );
        public static readonly OrganicSpecies FonticuluaUpupam = new OrganicSpecies( "Fonticulus_03", OrganicGenus.Fonticulus, 5727600,(decimal?)0.28, null, null, "IcyBody;RockyBody","ArgonRich","","B;A;F;G;K;M;L;T;TTS;Y;D;N;AEBE" );
        public static readonly OrganicSpecies FrutexaAcus = new OrganicSpecies( "Shrubs_02", OrganicGenus.Shrubs, 7774700,(decimal?)0.28, null, (decimal?)195, "RockyBody","CarbonDioxide;CarbonDioxideRich","","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FrutexaCollum = new OrganicSpecies( "Shrubs_07", OrganicGenus.Shrubs, 1639800,(decimal?)0.28, null, null, "RockyBody","SulphurDioxide","","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FrutexaFera = new OrganicSpecies( "Shrubs_05", OrganicGenus.Shrubs, 1632500,(decimal?)0.28, null, (decimal?)195, "RockyBody","CarbonDioxide;CarbonDioxideRich","None","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FrutexaFlabellum = new OrganicSpecies( "Shrubs_01", OrganicGenus.Shrubs, 1808900,(decimal?)0.28, null, null, "RockyBody","Ammonia","","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FrutexaFlammasis = new OrganicSpecies( "Shrubs_04", OrganicGenus.Shrubs, 10326000,(decimal?)0.28, null, null, "RockyBody","Ammonia","","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FrutexaMetallicum = new OrganicSpecies( "Shrubs_03", OrganicGenus.Shrubs, 1632500,(decimal?)0.28, null, (decimal?)195, "HighMetalContentBody","CarbonDioxide;CarbonDioxideRich;Ammonia","None","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FrutexaSponsae = new OrganicSpecies( "Shrubs_06", OrganicGenus.Shrubs, 5988000,(decimal?)0.28, null, null, "RockyBody","Water;WaterRich","","B;F;G;M;L;TTS;D;N" );
        public static readonly OrganicSpecies FumerolaAquatis = new OrganicSpecies( "Fumerolas_04", OrganicGenus.Fumerolas, 6284600,(decimal?)0.28, null, (decimal?)450, "IcyBody;RockyIceBody","Any","Water","" );
        public static readonly OrganicSpecies FumerolaCarbosis = new OrganicSpecies( "Fumerolas_01", OrganicGenus.Fumerolas, 6284600,(decimal?)0.28, null, (decimal?)275, "IcyBody;RockyIceBody","Any","Carbon;Methane","" );
        public static readonly OrganicSpecies FumerolaExtremus = new OrganicSpecies( "Fumerolas_02", OrganicGenus.Fumerolas, 16202800,(decimal?)0.28, null, (decimal?)205, "RockyBody;HighMetalContentBody","Any","Silicate;Iron;Rocky","" );
        public static readonly OrganicSpecies FumerolaNitris = new OrganicSpecies( "Fumerolas_03", OrganicGenus.Fumerolas, 7500900,(decimal?)0.28, null, (decimal?)250, "IcyBody;RockyIceBody","Any","Nitrogen;Ammonia","" );
        public static readonly OrganicSpecies FungoidaBullarum = new OrganicSpecies( "Fungoids_03", OrganicGenus.Fungoids, 3703200,(decimal?)0.28, null, null, "RockyBody;HighMetalContentBody;RockyIceBody","Argon;ArgonRich","None","" );
        public static readonly OrganicSpecies FungoidaGelata = new OrganicSpecies( "Fungoids_04", OrganicGenus.Fungoids, 3330300,(decimal?)0.28, (decimal?)180, (decimal?)195, "RockyBody;HighMetalContentBody;RockyIceBody","Water;WaterRich;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies FungoidaSetisis = new OrganicSpecies( "Fungoids_01", OrganicGenus.Fungoids, 1670100,(decimal?)0.28, null, null, "RockyBody;HighMetalContentBody;RockyIceBody","Ammonia;Methane;MethaneRich","","" );
        public static readonly OrganicSpecies FungoidaStabitis = new OrganicSpecies( "Fungoids_02", OrganicGenus.Fungoids, 2680300,(decimal?)0.28, (decimal?)180, (decimal?)195, "RockyBody;HighMetalContentBody;RockyIceBody","Water;WaterRich;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies OsseusCornibus = new OrganicSpecies( "Osseus_05", OrganicGenus.Osseus, 1483000,(decimal?)0.28, (decimal?)180, (decimal?)195, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","" );
        public static readonly OrganicSpecies OsseusDiscus = new OrganicSpecies( "Osseus_02", OrganicGenus.Osseus, 12934900,(decimal?)0.28, null, (decimal?)455, "RockyBody;HighMetalContentBody","Water;WaterRich","","" );
        public static readonly OrganicSpecies OsseusFractus = new OrganicSpecies( "Osseus_01", OrganicGenus.Osseus, 4027800,(decimal?)0.28, (decimal?)180, (decimal?)190, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","" );
        public static readonly OrganicSpecies OsseusPellebantus = new OrganicSpecies( "Osseus_06", OrganicGenus.Osseus, 9739000,(decimal?)0.28, (decimal?)190, (decimal?)195, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","" );
        public static readonly OrganicSpecies OsseusPumice = new OrganicSpecies( "Osseus_04", OrganicGenus.Osseus, 3156300,(decimal?)0.28, null, (decimal?)135, "RockyBody;HighMetalContentBody;RockyIceBody","Argon;ArgonRich;Methane;MethaneRich;Nitrogen","","" );
        public static readonly OrganicSpecies OsseusSpiralis = new OrganicSpecies( "Osseus_03", OrganicGenus.Osseus, 2404700,(decimal?)0.28, (decimal?)160, null, "RockyBody;HighMetalContentBody","Ammonia","","" );
        public static readonly OrganicSpecies ReceptaConditivus = new OrganicSpecies( "Recepta_03", OrganicGenus.Recepta, 14313700,(decimal?)0.28, (decimal?)130, (decimal?)300, "IcyBody;RockyIceBody","SulphurDioxide","","" );
        public static readonly OrganicSpecies ReceptaDeltahedronix = new OrganicSpecies( "Recepta_02", OrganicGenus.Recepta, 16202800,(decimal?)0.28, (decimal?)130, (decimal?)300, "RockyBody;HighMetalContentBody","SulphurDioxide","","" );
        public static readonly OrganicSpecies ReceptaUmbrux = new OrganicSpecies( "Recepta_01", OrganicGenus.Recepta, 12934900,(decimal?)0.28, (decimal?)130, (decimal?)300, "IcyBody;RockyIceBody;RockyBody;HighMetalContentBody","SulphurDioxide","","" );
        public static readonly OrganicSpecies AlbidumSinuousTubers = new OrganicSpecies( "TubeABCD_02", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","None","Any","" );
        public static readonly OrganicSpecies BlatteumSinuousTubers = new OrganicSpecies( "TubeEFGH", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","SulphurDioxide;None","Any","" );
        public static readonly OrganicSpecies CaeruleumSinuousTubers = new OrganicSpecies( "TubeABCD_03", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","SulphurDioxide;None","Any","" );
        public static readonly OrganicSpecies LindigoticumSinuousTubers = new OrganicSpecies( "TubeEFGH_01", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","None","Any","" );
        public static readonly OrganicSpecies PrasinumSinuousTubers = new OrganicSpecies( "TubeABCD_01", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody;RockyIceBody","CarbonDioxideRich;None;CarbonDioxide;SulphurDioxide","Any","" );
        public static readonly OrganicSpecies RoseumSinuousTubers = new OrganicSpecies( "Tube", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich;ArgonRich;SulphurDioxide;None","Any","" );
        public static readonly OrganicSpecies ViolaceumSinuousTubers = new OrganicSpecies( "TubeEFGH_02", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","None","Any","" );
        public static readonly OrganicSpecies VirideSinuousTubers = new OrganicSpecies( "TubeEFGH_03", OrganicGenus.Tubers, 1514500,null, (decimal?)200, (decimal?)500, "RockyBody;HighMetalContentBody","SulphurDioxide;None","Any","" );
        public static readonly OrganicSpecies StratumAraneamus = new OrganicSpecies( "Stratum_04", OrganicGenus.Stratum, 2448900,(decimal?)0.55, (decimal?)165, null, "RockyBody","SulphurDioxide","","" );
        public static readonly OrganicSpecies StratumCucumisis = new OrganicSpecies( "Stratum_06", OrganicGenus.Stratum, 16202800,(decimal?)0.6, (decimal?)190, null, "RockyBody","SulphurDioxide;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies StratumExcutitus = new OrganicSpecies( "Stratum_01", OrganicGenus.Stratum, 2448900,(decimal?)0.48, (decimal?)165, (decimal?)190, "RockyBody","SulphurDioxide;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies StratumFrigus = new OrganicSpecies( "Stratum_08", OrganicGenus.Stratum, 2637500,(decimal?)0.55, (decimal?)190, null, "RockyBody","SulphurDioxide;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies StratumLaminamus = new OrganicSpecies( "Stratum_03", OrganicGenus.Stratum, 2788300,(decimal?)0.34, (decimal?)165, null, "RockyBody","Ammonia","","" );
        public static readonly OrganicSpecies StratumLimaxus = new OrganicSpecies( "Stratum_05", OrganicGenus.Stratum, 1362000,(decimal?)0.48, (decimal?)165, (decimal?)190, "RockyBody","SulphurDioxide;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies StratumPaleas = new OrganicSpecies( "Stratum_02", OrganicGenus.Stratum, 1362000,(decimal?)0.58, (decimal?)165, null, "RockyBody","Ammonia;Water;WaterRich;CarbonDioxide;CarbonDioxideRich","","" );
        public static readonly OrganicSpecies StratumTectonicas = new OrganicSpecies( "Stratum_07", OrganicGenus.Stratum, 19010800,(decimal?)0.9, (decimal?)165, null, "HighMetalContentBody","Oxygen;Ammonia;Water;WaterRich;CarbonDioxide;CarbonDioxideRich;SulphurDioxide","","" );
        public static readonly OrganicSpecies TubusCavas = new OrganicSpecies( "Tubus_03", OrganicGenus.Tubus, 11873200,(decimal?)0.16, (decimal?)160, (decimal?)200, "RockyBody","CarbonDioxide","None","F;G;H;A;K;N;M;B" );
        public static readonly OrganicSpecies TubusCompagibus = new OrganicSpecies( "Tubus_05", OrganicGenus.Tubus, 7774700,(decimal?)0.19, (decimal?)150, (decimal?)190, "RockyBody","CarbonDioxide","None","S;A;K;M;N;M;DC;H;K" );
        public static readonly OrganicSpecies TubusConifer = new OrganicSpecies( "Tubus_01", OrganicGenus.Tubus, 2415500,(decimal?)0.17, (decimal?)160, (decimal?)200, "RockyBody","CarbonDioxide","None","F;G;A;K;N;M;H" );
        public static readonly OrganicSpecies TubusRosarium = new OrganicSpecies( "Tubus_04", OrganicGenus.Tubus, 2637500,(decimal?)0.16, (decimal?)160, (decimal?)180, "RockyBody","Ammonia","","F;G;A;K;N;B;K" );
        public static readonly OrganicSpecies TubusSororibus = new OrganicSpecies( "Tubus_02", OrganicGenus.Tubus, 5727600,(decimal?)0.16, (decimal?)160, (decimal?)200, "HighMetalContentBody","Ammonia;CarbonDioxide","None","F;G;A;L;K;N;M;M;DC" );
        public static readonly OrganicSpecies TussockAlbata = new OrganicSpecies( "Tussocks_08", OrganicGenus.Tussocks, 3252500,(decimal?)0.28, (decimal?)175, (decimal?)180, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockCapillum = new OrganicSpecies( "Tussocks_15", OrganicGenus.Tussocks, 7025800,(decimal?)0.28, (decimal?)80, (decimal?)165, "RockyBody;RockyIceBody","Argon;ArgonRich;Methane;MethaneRich","","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockCaputus = new OrganicSpecies( "Tussocks_11", OrganicGenus.Tussocks, 3472400,(decimal?)0.28, (decimal?)180, (decimal?)190, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockCatena = new OrganicSpecies( "Tussocks_05", OrganicGenus.Tussocks, 1766600,(decimal?)0.28, (decimal?)150, (decimal?)190, "RockyBody;HighMetalContentBody","Ammonia","","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockCultro = new OrganicSpecies( "Tussocks_04", OrganicGenus.Tussocks, 1766600,(decimal?)0.28, null, null, "RockyBody;HighMetalContentBody","Ammonia","","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockDivisa = new OrganicSpecies( "Tussocks_10", OrganicGenus.Tussocks, 1766600,(decimal?)0.28, (decimal?)150, (decimal?)180, "RockyBody;HighMetalContentBody","Ammonia","","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockIgnis = new OrganicSpecies( "Tussocks_03", OrganicGenus.Tussocks, 1849000,(decimal?)0.28, (decimal?)160, (decimal?)170, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockPennata = new OrganicSpecies( "Tussocks_01", OrganicGenus.Tussocks, 5853800,(decimal?)0.28, (decimal?)145, (decimal?)155, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockPennatis = new OrganicSpecies( "Tussocks_06", OrganicGenus.Tussocks, 1000000,(decimal?)0.28, null, (decimal?)195, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockPropagito = new OrganicSpecies( "Tussocks_09", OrganicGenus.Tussocks, 1000000,(decimal?)0.28, null, (decimal?)195, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockSerrati = new OrganicSpecies( "Tussocks_07", OrganicGenus.Tussocks, 4447100,(decimal?)0.28, (decimal?)170, (decimal?)175, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockStigmasis = new OrganicSpecies( "Tussocks_13", OrganicGenus.Tussocks, 19010800,(decimal?)0.28, (decimal?)130, (decimal?)210, "RockyBody;HighMetalContentBody","SulphurDioxide","","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockTriticum = new OrganicSpecies( "Tussocks_12", OrganicGenus.Tussocks, 7774700,(decimal?)0.28, (decimal?)190, (decimal?)195, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","None","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockVentusa = new OrganicSpecies( "Tussocks_02", OrganicGenus.Tussocks, 3227700,(decimal?)0.28, (decimal?)155, (decimal?)160, "RockyBody;HighMetalContentBody","CarbonDioxide;CarbonDioxideRich","","F;G;K;M;L;T;D;H" );
        public static readonly OrganicSpecies TussockVirgam = new OrganicSpecies( "Tussocks_14", OrganicGenus.Tussocks, 14313700,(decimal?)0.28, (decimal?)390, (decimal?)450, "RockyBody;HighMetalContentBody","Water;WaterRich","","F;G;K;M;L;T;D;H" );


        // Species without any known criteria (including non-terrestrial species)
        public static readonly OrganicSpecies SolidMineralSpheres = new OrganicSpecies( "SPOI", OrganicGenus.MineralSpheres, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LatticeMineralSpheres = new OrganicSpecies( "SPOI_Ball", OrganicGenus.MineralSpheres, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PrasinumMetallicCrystals = new OrganicSpecies( "L_Cry_MetCry", OrganicGenus.MetallicCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PurpureumMetallicCrystals = new OrganicSpecies( "L_Cry_MetCry", OrganicGenus.MetallicCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumMetallicCrystals = new OrganicSpecies( "L_Cry_MetCry", OrganicGenus.MetallicCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies FlavumMetallicCrystals = new OrganicSpecies( "L_Cry_MetCry", OrganicGenus.MetallicCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbidumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PrasinumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RoseumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PurpureumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies FlavumSilicateCrystals = new OrganicSpecies( "L_Cry_QtzCry", OrganicGenus.SilicateCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbidumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PrasinumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RoseumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PurpureumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies FlavumIceCrystals = new OrganicSpecies( "L_Cry_IcCry", OrganicGenus.IceCrystals, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumReelMollusc = new OrganicSpecies( "L_Org_Moll03_V6", OrganicGenus.MolluscReel, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumReelMollusc = new OrganicSpecies( "L_Org_Moll03_V6", OrganicGenus.MolluscReel, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideReelMollusc = new OrganicSpecies( "L_Org_Moll03_V6", OrganicGenus.MolluscReel, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CobalteumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies NiveumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PrasinumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RoseumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies OstrinumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RutulumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CroceumGlobeMollusc = new OrganicSpecies( "Small_Org_Moll01_V5", OrganicGenus.MolluscGlobe, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies GypseeumBellMollusc = new OrganicSpecies( "Small_Org_Moll01_V6", OrganicGenus.MolluscBell, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbensBellMollusc = new OrganicSpecies( "Small_Org_Moll01_V6", OrganicGenus.MolluscBell, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies BlatteumBellMollusc = new OrganicSpecies( "Small_Org_Moll01_V6", OrganicGenus.MolluscBell, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumBellMollusc = new OrganicSpecies( "Small_Org_Moll01_V6", OrganicGenus.MolluscBell, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumBellMollusc = new OrganicSpecies( "Small_Org_Moll01_V6", OrganicGenus.MolluscBell, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumUmbrellaMollusc = new OrganicSpecies( "L_Org_Moll03_V3", OrganicGenus.MolluscUmbrella, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumUmbrellaMollusc = new OrganicSpecies( "L_Org_Moll03_V3", OrganicGenus.MolluscUmbrella, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideUmbrellaMollusc = new OrganicSpecies( "L_Org_Moll03_V3", OrganicGenus.MolluscUmbrella, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbulumGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PhoeniceumGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PurpureumGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RufumGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CroceumGourdMollusc = new OrganicSpecies( "Small_Org_Moll01_V1", OrganicGenus.MolluscGourd, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideTorusMollusc = new OrganicSpecies( "Small_Org_Moll01_V2", OrganicGenus.MolluscTorus, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies BlatteumTorusMollusc = new OrganicSpecies( "Small_Org_Moll01_V2", OrganicGenus.MolluscTorus, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies FlavumTorusMollusc = new OrganicSpecies( "Small_Org_Moll01_V2", OrganicGenus.MolluscTorus, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumTorusMollusc = new OrganicSpecies( "Small_Org_Moll01_V2", OrganicGenus.MolluscTorus, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubellumTorusMollusc = new OrganicSpecies( "Small_Org_Moll01_V2", OrganicGenus.MolluscTorus, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumBulbMollusc = new OrganicSpecies( "L_Org_Moll03_V2", OrganicGenus.MolluscBulb, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumBulbMollusc = new OrganicSpecies( "L_Org_Moll03_V2", OrganicGenus.MolluscBulb, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideBulbMollusc = new OrganicSpecies( "L_Org_Moll03_V2", OrganicGenus.MolluscBulb, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumParasolMollusc = new OrganicSpecies( "L_Org_Moll03_V1", OrganicGenus.MolluscParasol, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumParasolMollusc = new OrganicSpecies( "L_Org_Moll03_V1", OrganicGenus.MolluscParasol, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideParasolMollusc = new OrganicSpecies( "L_Org_Moll03_V1", OrganicGenus.MolluscParasol, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbulumSquidMollusc = new OrganicSpecies( "Small_Org_Moll01_V3", OrganicGenus.MolluscSquid, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumSquidMollusc = new OrganicSpecies( "Small_Org_Moll01_V3", OrganicGenus.MolluscSquid, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PuniceumSquidMollusc = new OrganicSpecies( "Small_Org_Moll01_V3", OrganicGenus.MolluscSquid, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumSquidMollusc = new OrganicSpecies( "Small_Org_Moll01_V3", OrganicGenus.MolluscSquid, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RoseumSquidMollusc = new OrganicSpecies( "Small_Org_Moll01_V3", OrganicGenus.MolluscSquid, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CereumBulletMollusc = new OrganicSpecies( "Small_Org_Moll01_V4", OrganicGenus.MolluscBullet, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LividumBulletMollusc = new OrganicSpecies( "Small_Org_Moll01_V4", OrganicGenus.MolluscBullet, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideBulletMollusc = new OrganicSpecies( "Small_Org_Moll01_V4", OrganicGenus.MolluscBullet, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumBulletMollusc = new OrganicSpecies( "Small_Org_Moll01_V4", OrganicGenus.MolluscBullet, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies FlavumBulletMollusc = new OrganicSpecies( "Small_Org_Moll01_V4", OrganicGenus.MolluscBullet, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideCapsuleMollusc = new OrganicSpecies( "L_Org_Moll03_V4", OrganicGenus.MolluscCapsule, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumCapsuleMollusc = new OrganicSpecies( "L_Org_Moll03_V4", OrganicGenus.MolluscCapsule, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumCapsuleMollusc = new OrganicSpecies( "L_Org_Moll03_V4", OrganicGenus.MolluscCapsule, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbidumCollaredPod = new OrganicSpecies( "S_Seed_SdTp04", OrganicGenus.CollaredPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LividumCollaredPod = new OrganicSpecies( "S_Seed_SdTp04", OrganicGenus.CollaredPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies BlatteumCollaredPod = new OrganicSpecies( "S_Seed_SdTp04", OrganicGenus.CollaredPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubicundumCollaredPod = new OrganicSpecies( "S_Seed_SdTp04", OrganicGenus.CollaredPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies StolonPod = new OrganicSpecies( "SPOI_Root", OrganicGenus.StolonPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies StolonTree = new OrganicSpecies( "L_Seed_SdRt02", OrganicGenus.StolonTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CereumAsterPod = new OrganicSpecies( "S_Seed_SdTp02", OrganicGenus.AsterPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumAsterPod = new OrganicSpecies( "S_Seed_SdTp02", OrganicGenus.AsterPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PrasinumAsterPod = new OrganicSpecies( "S_Seed_SdTp02", OrganicGenus.AsterPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PuniceumAsterPod = new OrganicSpecies( "S_Seed_SdTp02", OrganicGenus.AsterPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubellumAsterPod = new OrganicSpecies( "S_Seed_SdTp02", OrganicGenus.AsterPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumChalicePod = new OrganicSpecies( "S_Seed_SdTp05", OrganicGenus.ChalicePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideChalicePod = new OrganicSpecies( "S_Seed_SdTp05", OrganicGenus.ChalicePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubellumChalicePod = new OrganicSpecies( "S_Seed_SdTp05", OrganicGenus.ChalicePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbidumChalicePod = new OrganicSpecies( "S_Seed_SdTp05", OrganicGenus.ChalicePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies OstrinumChalicePod = new OrganicSpecies( "S_Seed_SdTp05", OrganicGenus.ChalicePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CandidumPedunclePod = new OrganicSpecies( "S_Seed_SdTp01", OrganicGenus.PedunclePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumPedunclePod = new OrganicSpecies( "S_Seed_SdTp01", OrganicGenus.PedunclePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies GypseeumPedunclePod = new OrganicSpecies( "S_Seed_SdTp01", OrganicGenus.PedunclePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PurpureumPedunclePod = new OrganicSpecies( "S_Seed_SdTp01", OrganicGenus.PedunclePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RufumPedunclePod = new OrganicSpecies( "S_Seed_SdTp01", OrganicGenus.PedunclePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CandidumRhizomePod = new OrganicSpecies( "S_Seed_SdTp07", OrganicGenus.RhizomePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CobalteumRhizomePod = new OrganicSpecies( "S_Seed_SdTp07", OrganicGenus.RhizomePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies GypseeumRhizomePod = new OrganicSpecies( "S_Seed_SdTp07", OrganicGenus.RhizomePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PurpureumRhizomePod = new OrganicSpecies( "S_Seed_SdTp07", OrganicGenus.RhizomePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumRhizomePod = new OrganicSpecies( "S_Seed_SdTp07", OrganicGenus.RhizomePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbidumQuadripartitePod = new OrganicSpecies( "S_Seed_SdTp08", OrganicGenus.QuadripartitePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumQuadripartitePod = new OrganicSpecies( "S_Seed_SdTp08", OrganicGenus.QuadripartitePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideQuadripartitePod = new OrganicSpecies( "S_Seed_SdTp08", OrganicGenus.QuadripartitePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies BlatteumQuadripartitePod = new OrganicSpecies( "S_Seed_SdTp08", OrganicGenus.QuadripartitePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies NiveumOctahedralPod = new OrganicSpecies( "S_Seed_SdTp03", OrganicGenus.VoidPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies BlatteumOctahedralPod = new OrganicSpecies( "S_Seed_SdTp03", OrganicGenus.VoidPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumOctahedralPod = new OrganicSpecies( "S_Seed_SdTp03", OrganicGenus.VoidPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideOctahedralPod = new OrganicSpecies( "S_Seed_SdTp03", OrganicGenus.VoidPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubeumOctahedralPod = new OrganicSpecies( "S_Seed_SdTp03", OrganicGenus.VoidPod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CereumAsterTree = new OrganicSpecies( "L_Seed_Pln02_V3", OrganicGenus.AsterTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies PrasinumAsterTree = new OrganicSpecies( "L_Seed_Pln02_V3", OrganicGenus.AsterTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubellumAsterTree = new OrganicSpecies( "L_Seed_Pln02_V3", OrganicGenus.AsterTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AlbidumPeduncleTree = new OrganicSpecies( "L_Seed_Pln01_V1", OrganicGenus.PeduncleTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CaeruleumPeduncleTree = new OrganicSpecies( "L_Seed_Pln01_V1", OrganicGenus.PeduncleTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies ViridePeduncleTree = new OrganicSpecies( "L_Seed_Pln01_V1", OrganicGenus.PeduncleTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies OstrinumPeduncleTree = new OrganicSpecies( "L_Seed_Pln01_V1", OrganicGenus.PeduncleTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RubellumPeduncleTree = new OrganicSpecies( "L_Seed_Pln01_V1", OrganicGenus.PeduncleTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AurariumGyreTree = new OrganicSpecies( "SPOI_SeedPolyp01_V1", OrganicGenus.GyreTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideGyreTree = new OrganicSpecies( "SPOI_SeedPolyp01", OrganicGenus.GyreTree, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RoseumGyrePod = new OrganicSpecies( "S_Seed_SdTp06", OrganicGenus.GyrePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies AurariumGyrePod = new OrganicSpecies( "S_Seed_SdTp06", OrganicGenus.GyrePod, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies ChryseumVoidHeart = new OrganicSpecies( "SPOI_SeedWeed01", OrganicGenus.VoidHeart, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies RutulumCalcitePlates = new OrganicSpecies( "L_Org_PltFun_V1", OrganicGenus.CalcitePlates, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LuteolumCalcitePlates = new OrganicSpecies( "L_Org_PltFun_V1", OrganicGenus.CalcitePlates, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LindigoticumCalcitePlates = new OrganicSpecies( "L_Org_PltFun_V1", OrganicGenus.CalcitePlates, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies VirideCalcitePlates = new OrganicSpecies( "L_Org_PltFun_V1", OrganicGenus.CalcitePlates, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies CommonThargoidBarnacle = new OrganicSpecies( "Thargoid_Barnacle", OrganicGenus.ThargoidBarnacle, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies LargeThargoidBarnacle = new OrganicSpecies( "Thargoid_Barnacle", OrganicGenus.ThargoidBarnacle, 50000,null, null, null, "","","","" );
        public static readonly OrganicSpecies ThargoidBarnacleBarbs = new OrganicSpecies( "Thargoid_Barnacle", OrganicGenus.ThargoidBarnacle, 50000,null, null, null, "","","","" );


        public OrganicGenus genus;

        [PublicAPI( "The credit value for this species" )]
        public long value;
        
        public decimal? maxG;
        public decimal? minK;
        public decimal? maxK;
        public IList<string> planetClass;
        public IList<string> atmosphereClass;
        public IList<string> starClass;
        public IList<string> volcanism;

        [JsonIgnore, PublicAPI]
        public string localizedDescription => Properties.OrganicSpeciesDesc.ResourceManager.GetString( edname );

        [JsonIgnore, PublicAPI]
        public string localizedConditions => Properties.OrganicSpeciesCond.ResourceManager.GetString( edname );

        [JsonIgnore]
        public bool isPredictable => maxG != null ||
                                     minK != null ||
                                     maxK != null ||
                                     planetClass.Any() ||
                                     atmosphereClass.Any() ||
                                     volcanism.Any() ||
                                     starClass.Any();

        // dummy used to ensure that the static constructor has run
        public OrganicSpecies () : this( "" )
        { }

        private OrganicSpecies ( string edname ) : base( edname, edname )
        {
            this.planetClass = new List<string>();
            this.atmosphereClass = new List<string>();
            this.starClass = new List<string>();
            this.volcanism = new List<string>();
        }

        private OrganicSpecies ( string edname,
                                 OrganicGenus genus,
                                 long value,
                                 decimal? maxG,
                                 decimal? minK,
                                 decimal? maxK,
                                 string planetClass,
                                 string atmosphereClass,
                                 string volcanism,
                                 string starClass ) : base( edname, NormalizeSpecies( edname ) )
        {
            this.genus = genus;
            this.value = value;
            this.maxG = maxG;
            this.minK = minK;
            this.maxK = maxK;
            this.planetClass = !string.IsNullOrEmpty( planetClass ) ? planetClass.Split( ';' ).ToList() : new List<string>();
            this.atmosphereClass = !string.IsNullOrEmpty( atmosphereClass ) ? atmosphereClass.Split( ';' ).ToList() : new List<string>();
            this.starClass = !string.IsNullOrEmpty( starClass ) ? starClass.Split( ';' ).ToList() : new List<string>();
            this.volcanism = !string.IsNullOrEmpty( volcanism ) ? volcanism.Split( ';' ).ToList() : new List<string>();
        }

        public static new OrganicSpecies FromEDName ( string edname )
        {
            return ResourceBasedLocalizedEDName<OrganicSpecies>.FromEDName( NormalizeSpecies( edname ) );
        }

        public static string NormalizeSpecies ( string edname )
        {
            return edname?
                .Replace( "Codex_Ent_", "" )
                .Replace( "$", "" )
                .Replace( "_Name;", "" )
                .Replace( "_name;", "" )
                .Replace( ";", "" );
        }
    }
}
