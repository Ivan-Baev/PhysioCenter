namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ServicesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Services.Any())
            {
                return;
            }

            var services = new Service[]
                {
                    //1. Massages
                    new Service
                    {
                        Name = "Myofascial Release",
                        Description = "Myofascial release is manual technique for stretching the fascia aiming to release fascia restrictions.. Fascia is located between the skin and the underlying structure of muscle and bone, and connects the muscles, organs, and skeletal structures in our body. Fascia can become restricted through injuries, stress, trauma, and poor posture.",
                        CategoryId = new Guid("4e597ea2-afa0-4916-bb39-3736d62ee22c"),
                        Price = 30,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787185/PhysioCenter%20Images/MayoFacialRelease-2_r54g0h.jpg"
                    },

                    new Service
                    {
                        Name = "Trigger Point Therapy",
                        Description = "Trigger point therapy involves the applying of pressure to tender muscle tissue in order to relieve pain and dysfunction in other parts of the body. Trigger points are active centres of muscular hyperactivity, which often cross-over with acupuncture points. The video below shows how a client can do self trigger point massage using a small ball.",
                        CategoryId = new Guid("4e597ea2-afa0-4916-bb39-3736d62ee22c"),
                        Price = 40,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787258/PhysioCenter%20Images/trigger-pointtherapy_bnqvvo.webp"
                    },

                    new Service
                    {
                        Name = "Compression Massage",
                        Description = "Rhythmic compression into muscles used to create a deep hyperaemia and softening effect in the tissues. Often used for sports massage as a warm-up for deeper, more specific massage work.",
                        CategoryId = new Guid("4e597ea2-afa0-4916-bb39-3736d62ee22c"),
                        Price = 25,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787307/PhysioCenter%20Images/Pressotherapy_____tjpvn6.jpg"
                    },

                    new Service
                    {
                        Name = "Cross-Fibre Massage",
                        Description = "Cross-fibre friction techniques applied in a general manner to create a stretching and broadening effect in large muscle groups; or on site-specific muscle and connective tissue, deep transverse friction applied to reduce adhesions and to help create strong, flexible repair during the healing process.",
                        CategoryId = new Guid("4e597ea2-afa0-4916-bb39-3736d62ee22c"),
                        Price = 35,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787358/PhysioCenter%20Images/massage_adobe-53_iemrtn.jpg"
                    },

                    // 2. Sports Therapy
                    new Service
                    {
                        Name = "Kinesiology taping",
                        Description = "therapeutic taping technique which alleviates pain and facilitates lymphatic drainage by microscopically lifting the skin. This lifting affect forms convolutions in the skin increasing interstitial space and allowing for decreased inflammation in affected areas.",
                        CategoryId = new Guid("cf72a45c-c195-4ca4-b79a-87392cacf9b5"),
                        Price = 25,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787397/PhysioCenter%20Images/taping_brmupv.webp"
                    },
                    new Service
                    {
                        Name = "Postural assessment and correction",
                        Description = "Your therapist will show you how to carry out exercises and movements in positions of correct posture in order to prevent injury and perform at your best.",
                        CategoryId = new Guid("cf72a45c-c195-4ca4-b79a-87392cacf9b5"),
                        Price = 50,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787460/PhysioCenter%20Images/postural-assessment_x7agxw.jpg"
                    },

                    // 3. Physiotherapy
                    new Service
                    {
                        Name = "Manual therapy",
                        Description = "Manual therapy is a technique used by physiotherapists to manipulate and mobilize affected joints by massaging them with the use of their hands.",
                        CategoryId = new Guid("5f176df0-2547-455c-bd46-b87e58107dc1"),
                        Price = 55,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787510/PhysioCenter%20Images/manualPTpic-7fab7a3fb16d416f8482a14adeba0c01_iohqe1.webp"
                    },
                    new Service
                    {
                        Name = "TENS therapy",
                        Description = "Transcutaneous electrical nerve stimulation (TENS) therapy – It is a technique wherein a small battery-driven device is used to send low-grade current through the electrodes placed on the skin surface. A TENS device temporarily relieves the pain of the affected area.",
                        CategoryId = new Guid("5f176df0-2547-455c-bd46-b87e58107dc1"),
                        Price = 35,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787578/PhysioCenter%20Images/2304084_e16llg.jpg"
                    },
                    new Service
                    {
                        Name = "Dry needling and acupuncture",
                        Description = "Fine needles are inserted into specific body points, which reduce pain for a short span of time.",
                        CategoryId = new Guid("5f176df0-2547-455c-bd46-b87e58107dc1"),
                        Price = 50,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787650/PhysioCenter%20Images/dry-needling-vs-acupuncture-treatment_y0lawq.jpg"
                    },
                    new Service
                    {
                        Name = "Hydrotherapy",
                        Description = "Hydrotherapy utilizes water to treat arthritis. Specialized exercises are performed inside water with a temperature range of 33-36 degree Celsius under the guidance of a physiotherapist. It involves various stretching, aerobics, and strengthening exercises.",
                        CategoryId = new Guid("5f176df0-2547-455c-bd46-b87e58107dc1"),
                        Price = 55,
                        ImageUrl = "https://res.cloudinary.com/physiocenter/image/upload/v1649787690/PhysioCenter%20Images/iStock-172976360-1024x683_lefj8c.jpg"
                    },
                };

            // Need them in particular order
            foreach (var service in services)
            {
                await dbContext.AddAsync(service);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}