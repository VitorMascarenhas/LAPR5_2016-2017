namespace PortoGO.DB.Migrations
{
    using Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PortoGO.DB.PortoGoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PortoGoContext context)
        {
            SeedRoles(context);

            SeedUsers(context);

            AddUserToRole(context);

            //SeedLocations(context);

            //SeedPoi(context);
        }

        private void SeedRoles(PortoGoContext context)
        {
            context.Roles.AddOrUpdate(
                p => p.Name,
                new IdentityRole("Admin")
            );
        }

        private void SeedUsers(PortoGoContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var user = new User()
                {
                    UserName = "admin",
                    Email = "admin@google.com",
                    DisplayName = "Administrator"
                };

                userManager.Create(user, "Password01!");
                userManager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "porto3d"))
            {
                var user = new User()
                {
                    UserName = "porto3d",
                    Email = "porto3d@google.com",
                    DisplayName = "Porto 3D"
                };

                userManager.Create(user, "porto3d");
                userManager.AddToRole(user.Id, "Admin");
            }
        }

        private void AddUserToRole(PortoGoContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            User user = userManager.FindByName("Admin");

            try
            {
                userManager.AddToRole(user.Id, "Admin");
            }
            catch { }
        }


        //private void SeedLocations(PortoGoContext context)
        //{
        //    context.Locations.AddOrUpdate(
        //        p => p.Name,
        //        new Location("Torre dos Clérigos", 41.1456753f, -8.614598500000056f),
        //        new Location("ISEP", 41.17784570000001f, -8.608100599999943f),
        //        new Location("Estação de São Bento", 41.1442942f, -8.61059350000005f),
        //        new Location("Estação de Campanhã", 41.1487999f, -8.585402299999942f),
        //        new Location("Teste-25449245", 41.1471851f, -8.5853787f),
        //        new Location("Teste-25449246", 41.1477198f, -8.5852170f),
        //        new Location("Teste-25449247", 41.1507225f, -8.5845591f),
        //        new Location("Teste-25449250", 41.1541300f, -8.5840688f),
        //        new Location("Teste-25449251", 41.1567511f, -8.5832999f),
        //        new Location("Teste-25449255", 41.1638290f, -8.5802570f),
        //        new Location("Teste-25449257", 41.1657318f, -8.5783023f),
        //        new Location("Teste-25449258", 41.1757128f, -8.5668188f),
        //        new Location("Teste-25449261", 41.1831167f, -8.5583031f),
        //        new Location("Teste-25449262", 41.1840308f, -8.5575022f),
        //        new Location("Teste-25449263", 41.1850770f, -8.5568476f),
        //        new Location("Teste-25449264", 41.1865640f, -8.5563026f),
        //        new Location("Teste-25449265", 41.1887762f, -8.5555515f),
        //        new Location("Teste-25449267", 41.1895190f, -8.5553584f),
        //        new Location("Teste-25449269", 41.1903264f, -8.5553155f),
        //        new Location("Teste-25449271", 41.1913922f, -8.5553799f),
        //        new Location("Teste-25449273", 41.1976618f, -8.5560299f),
        //        new Location("Teste-25449279", 41.1993144f, -8.5562148f),
        //        new Location("Teste-25449280", 41.2001396f, -8.5563647f),
        //        new Location("Teste-25449281", 41.2036967f, -8.5572912f),
        //        new Location("Teste-25449282", 41.2052824f, -8.5575470f),
        //        new Location("Teste-25449283", 41.2063589f, -8.5574827f),
        //        new Location("Teste-25503936", 41.1659814f, -8.6405050f),
        //        new Location("Teste-25503937", 41.1663352f, -8.6407599f),
        //        new Location("Teste-25503938", 41.1665984f, -8.6406541f),
        //        new Location("Teste-25503940", 41.1673010f, -8.6404167f),
        //        new Location("Teste-25503943", 41.1683007f, -8.6403026f),
        //        new Location("Teste-25503951", 41.1683470f, -8.6420446f),
        //        new Location("Teste-25503952", 41.1682238f, -8.6424873f),
        //        new Location("Teste-25503953", 41.1681519f, -8.6427092f),
        //        new Location("Teste-25503962", 41.1677439f, -8.6438248f),
        //        new Location("Teste-25503963", 41.1675709f, -8.6438606f),
        //        new Location("Teste-25503965", 41.1672801f, -8.6438177f),
        //        new Location("Teste-25503966", 41.1670819f, -8.6436726f),
        //        new Location("Teste-25503967", 41.1668521f, -8.6435160f),
        //        new Location("Teste-25503969", 41.1665997f, -8.6434938f),
        //        new Location("Teste-25503970", 41.1663142f, -8.6437907f),
        //        new Location("Teste-25503977", 41.1659322f, -8.6414200f),
        //        new Location("Teste-25503993", 41.1669295f, -8.6428465f),
        //        new Location("Teste-25503994", 41.1669617f, -8.6431414f),
        //        new Location("Teste-25503995", 41.1670147f, -8.6432498f),
        //        new Location("Teste-25503996", 41.1672823f, -8.6435503f),
        //        new Location("Teste-25503998", 41.1675207f, -8.6435717f),
        //        new Location("Teste-25503999", 41.1676499f, -8.6434218f),
        //        new Location("Teste-25504000", 41.1676979f, -8.6431748f),
        //        new Location("Teste-25504002", 41.1676804f, -8.6430669f),
        //        new Location("Teste-25504003", 41.1676312f, -8.6424866f),
        //        new Location("Teste-25504005", 41.1678815f, -8.6426046f),
        //        new Location("Teste-25504006", 41.1680965f, -8.6425191f),
        //        new Location("Teste-25504007", 41.1682332f, -8.6422727f),
        //        new Location("Teste-25504008", 41.1682170f, -8.6419616f),
        //        new Location("Teste-25504010", 41.1680797f, -8.6417363f),
        //        new Location("Teste-25504011", 41.1678718f, -8.6416642f),
        //        new Location("Teste-25504013", 41.1676392f, -8.6417929f),
        //        new Location("Teste-25504017", 41.1674806f, -8.6411798f),
        //        new Location("Teste-25504018", 41.1673868f, -8.6408185f),
        //        new Location("Teste-25504021", 41.1671886f, -8.6406898f),
        //        new Location("Teste-25504024", 41.1669070f, -8.6407815f),
        //        new Location("Teste-25504025", 41.1667713f, -8.6411247f),
        //        new Location("Teste-25504027", 41.1668241f, -8.6414316f),
        //        new Location("Teste-25504120", 41.1762333f, -8.6225925f),
        //        new Location("Teste-25504123", 41.1755536f, -8.6224240f),
        //        new Location("Teste-25504126", 41.1749160f, -8.6228025f),
        //        new Location("Teste-25504135", 41.1745264f, -8.6225730f),
        //        new Location("Teste-25504141", 41.1728439f, -8.6223490f),
        //        new Location("Teste-25504147", 41.1724849f, -8.6224639f),
        //        new Location("Teste-25504151", 41.1722948f, -8.6227460f),
        //        new Location("Teste-25504153", 41.1722948f, -8.6231859f),
        //        new Location("Teste-25504154", 41.1724805f, -8.6234970f),
        //        new Location("Teste-25504156", 41.1728439f, -8.6235721f),
        //        new Location("Teste-25504157", 41.1731439f, -8.6233222f),
        //        new Location("Teste-25504158", 41.1732085f, -8.6231591f),
        //        new Location("Teste-25504163", 41.1746710f, -8.6224487f),
        //        new Location("Teste-25504164", 41.1742772f, -8.6223748f),
        //        new Location("Teste-25504165", 41.1727167f, -8.6220456f),
        //        new Location("Teste-25504166", 41.1725112f, -8.6221063f),
        //        new Location("Teste-25504167", 41.1721596f, -8.6225438f),
        //        new Location("Teste-25504168", 41.1720932f, -8.6228926f),
        //        new Location("Teste-25504169", 41.1723485f, -8.6235747f),
        //        new Location("Teste-25504170", 41.1726560f, -8.6240516f),
        //        new Location("Teste-25504171", 41.1728750f, -8.6246354f),
        //        new Location("Teste-25504172", 41.1729503f, -8.6251884f),
        //        new Location("Teste-25504173", 41.1729371f, -8.6255381f),
        //        new Location("Teste-25504174", 41.1746091f, -8.6232340f),
        //        new Location("Teste-25504175", 41.1743320f, -8.6237194f),
        //        new Location("Teste-25504176", 41.1737495f, -8.6240683f),
        //        new Location("Teste-25504177", 41.1734635f, -8.6243241f),
        //        new Location("Teste-25504178", 41.1733707f, -8.6245069f),
        //        new Location("Teste-25504179", 41.1737301f, -8.6206520f),
        //        new Location("Teste-25504180", 41.1737976f, -8.6213756f),
        //        new Location("Teste-25504181", 41.1740318f, -8.6218803f),
        //        new Location("Teste-25504182", 41.1744024f, -8.6222471f),
        //        new Location("Teste-25504183", 41.1748294f, -8.6223575f),
        //        new Location("Teste-25507436", 41.1206037f, -8.5725049f),
        //        new Location("Teste-25507437", 41.1206983f, -8.5723830f),
        //        new Location("Teste-25507438", 41.1199688f, -8.5723428f),
        //        new Location("Teste-25507439", 41.1199426f, -8.5721899f),
        //        new Location("Teste-25507440", 41.1187541f, -8.5721852f),
        //        new Location("Teste-25507441", 41.1187710f, -8.5720409f),
        //        new Location("Teste-25507442", 41.1175723f, -8.5720504f),
        //        new Location("Teste-25507443", 41.1176111f, -8.5719041f),
        //        new Location("Teste-25507444", 41.1170247f, -8.5719539f),
        //        new Location("Teste-25507445", 41.1168190f, -8.5717512f),
        //        new Location("Teste-25507446", 41.1158770f, -8.5715006f),
        //        new Location("Teste-25507447", 41.1159723f, -8.5713811f),
        //        new Location("Teste-25507448", 41.1150363f, -8.5708998f),
        //        new Location("Teste-25507449", 41.1150974f, -8.5707638f),
        //        new Location("Teste-25507450", 41.1129913f, -8.5690383f),
        //        new Location("Teste-25507451", 41.1131894f, -8.5690490f),
        //        new Location("Teste-25507452", 41.1117606f, -8.5679171f),
        //        new Location("Teste-25507453", 41.1117722f, -8.5677618f),
        //        new Location("Teste-25507454", 41.1103460f, -8.5666350f),
        //        new Location("Teste-25507455", 41.1104015f, -8.5664999f),
        //        new Location("Teste-25507456", 41.1096508f, -8.5661576f),
        //        new Location("Teste-25507457", 41.1097579f, -8.5660691f),
        //        new Location("Teste-25507459", 41.1089125f, -8.5656699f),
        //        new Location("Teste-25507550", 41.1663546f, -8.5838349f)

        //    );
        //}

        private void SeedPoi(PortoGoContext context)
        {
            var b = new BusinessHours { FromHour = new TimeSpan(9, 0, 0), ToHour = new TimeSpan(17, 59, 59) };

            var user = context.Users.Where(x => x.UserName == "admin").FirstOrDefault();

            context.PointsOfInterest.AddOrUpdate(
                p => p.LocationId,
                new PointOfInterest(1, "Placa junto à Torre dos Clérigos", b, 2.5, user),
                new PointOfInterest(2, "Edificio H", b, 6, user)
            );
        }


    }
}
