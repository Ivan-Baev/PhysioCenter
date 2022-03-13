namespace PhysioCenter.Infrastructure.Data.Seeding.CustomSeeders
{
    using PhysioCenter.Infrastructure.Data.Models;
    using PhysioCenter.Infrastructure.Data.Seeding;

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class BlogSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Blog.Any())
            {
                return;
            }

            var blogPosts = new Blog[]
                {
                    new Blog
                    {
                        Title = "To Ice Or Not To Ice An Injury?",
                        Content = @"Icing an injury typically takes place immediately after the injury occurs.  Using a cold compress or ice pack on a strained muscle can decrease inflammation and numb pain in the area.  Icing is effective at reducing pain and swelling because the cold constricts blood vessels and decreases circulation to the area.

For example, if an athlete rolls an ankle in a volleyball match an immediate application of ice will cut down on long-term swelling and potentially lessen recovery time.",
                        ImageUrl = "https://st2.depositphotos.com/1518767/7343/i/600/depositphotos_73431259-stock-photo-highlighted-bones-of-man-at.jpg"
                    },
                    new Blog
                    {
                        Title = "Pregnancy Massage at 38 Weeks?",
                        Content = @"Massage therapy during pregnancy can reduce anxiety, relieve muscular aches and joint pain, and above all improve the labour experience, and newborn health.

That said, is a massage still allowed past the 38-week mark, despite the fact that the babies due date is approaching? Yes, a pregnancy massage is still safe even after 38 weeks of pregnancy. However, if your doctor has advised you not to have a pregnant massage due to underlying health or medical concerns, you should not have one.

In fact, our highly experienced and qualified pregnancy massage therapist can help you cope and manage all the major changes and hormones in your body during each trimester which we know can be tough.  This article will focus solely on pregnancy massage at the 38-week stage, which is in the third trimester",
                        ImageUrl = "https://st2.depositphotos.com/1518767/7343/i/600/depositphotos_73431259-stock-photo-highlighted-bones-of-man-at.jpg"
                    },
                    new Blog
                    {
                        Title = "5 Ways to Recover the Lymphatic System Faster During Long COVID",
                        Content = @"Since the COVID outbreak at the beginning of 2020, our clinic has had many enquiries from those experiencing long-term post-viral reactions after contracting the virus.

Symptoms may include:

Persistent and extreme fatigue
Increased anxiety and depressive feelings
Swollen lymph nodes, particularly in the neck and axilla areas
Aches and pains
Poor memory, difficulty concentrating and lack of focus
These symptoms are similar to other medical conditions, such as ME and CFS, which can last for weeks, or even months. However, normally these symptoms of post-viral fatigue are expected to last for a short period of time.

COVID is seen as an aggressive virus that can weaken the immune and lymphatic systems. Unless a full recovery is made, energy levels will be reduced and fatal symptoms can remain. Even though there is still a lot to learn about COVID-19, health experts state the importance of resuming normal activities. They advise us to try to avoid resting too much. This will in turn help your immune system recover quicker.",
                        ImageUrl = "https://st2.depositphotos.com/1518767/7343/i/600/depositphotos_73431259-stock-photo-highlighted-bones-of-man-at.jpg"
                    },
                    new Blog
                    {
                        Title = "How the way we sit effects our back muscles",
                        Content = @"The discs in your spine expand and contract as you move, acting to absorb the pressure that occurs between your spinal vertebrae. This ‘shock’ absorption is important for keeping a healthy spine, allowing for movement.

Sitting puts more pressure on your spine than standing, and with this, your discs become compressed, which over time, can cause you to lose flexibility and increase the risk of herniated discs.

Sitting also causes your back muscles to become less active, which leads to weaker muscles over time. When you have a weak back and core muscles, your spine is less supported, which increases the likelihood of injury.

The sitting position also tightens your hip flexors and can restrict blood flow to the gluteal muscles, which are important spine supporters.

Sitting for prolonged periods of time can also mean that our posture changes, with a forward head position and rounded shoulders being prevalent. These posture changes can cause increased stress on the spinal ligaments to stretch beyond their healthy limit, increasing the wear on our discs. ",
                        ImageUrl = "https://st2.depositphotos.com/1518767/7343/i/600/depositphotos_73431259-stock-photo-highlighted-bones-of-man-at.jpg"
                    },
                    new Blog
                    {
                        Title = "How Often Should I Get A Massage For Stress Relief?",
                        Content = @"Stress Relief
The management of stress in this life that we are living is just as important to your health as diet and exercise. Indeed, stress can undo all of the hard work that you put in doing exercise and eating healthily.

Regular Swedish relaxation massage can help reduce tension, lower blood pressure and reduce cortisol levels in the body. The benefits of a relaxation massage are often felt immediately, for some as soon as they hit the massage couch they start to unwind. If you have tense muscles a little discomfort may be felt during the massage, but afterwards, your body will definitely thank you for it.

Poor Mental health can be greatly improved by relaxation massage allowing the client to feel calmer and less stressed.

Relaxation massage is lighter and less invasive than sports massage and can therefore be tolerated more often, so can be practised as often as the client requires.",
                        ImageUrl = "https://st2.depositphotos.com/1518767/7343/i/600/depositphotos_73431259-stock-photo-highlighted-bones-of-man-at.jpg"
                    },
                };

            await dbContext.AddRangeAsync(blogPosts);
        }
    }
}