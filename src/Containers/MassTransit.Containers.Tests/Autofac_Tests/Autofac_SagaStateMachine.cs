namespace MassTransit.Containers.Tests.Autofac_Tests
{
    using Autofac;
    using Common_Tests;
    using NUnit.Framework;
    using TestFramework.Sagas;


    public class Autofac_SagaStateMachine :
        Common_SagaStateMachine
    {
        readonly IContainer _container;

        public Autofac_SagaStateMachine()
        {
            var builder = new ContainerBuilder();
            builder.AddMassTransit(ConfigureRegistration);

            builder.RegisterType<PublishTestStartedActivity>();

            _container = builder.Build();
        }

        [OneTimeTearDown]
        public void Close_container()
        {
            _container.Dispose();
        }

        protected override void ConfigureSagaStateMachine(IInMemoryReceiveEndpointConfigurator configurator)
        {
            configurator.ConfigureSaga<TestInstance>(_container);
        }
    }
}
