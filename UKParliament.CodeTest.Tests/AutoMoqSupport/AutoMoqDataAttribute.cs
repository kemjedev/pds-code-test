using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace UKParliament.CodeTest.Tests.AutoMoqSupport
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        })
        {
        }
    }
}
