
namespace DirectOnTime.IntegrationTestFrame {

    using System;
    using System.Windows;

    using MassTransit;
    using StructureMap;
    using StructureMap.Pipeline;
    using DirectOnTime.Messages.Inbound;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly IServiceBus _bus;
        private UnsubscribeAction _unsubscribeAction;
        public MainWindow() {
            InitializeComponent();
            IContainer container = BootStrapContainer();
            _bus = container.GetInstance<IServiceBus>();
        }

        private void btnPaymentComplete_Click(Object sender, RoutedEventArgs eventArgs)
        {

            string clientId = txtClientId.Text;
            var message = new MonthlyPaymentCompleteMessage
                              {
                                  CorrelationId = Guid.NewGuid(),
                                  ClientId = clientId,
                                  BusinessUnit = "2009",
                                  ReceiptId = "RCPT10000011",
                                  UserName = "PGSKR"
                              };
            _bus.SubscribeInstance(this);
            _bus.Publish(message, x=>x.SetResponseAddress(_bus.Endpoint.Address.Uri));

        }


        private static IContainer BootStrapContainer() {
            var container = new Container();
            container.Configure(cfg => cfg.For<IServiceBus>()
                                           .LifecycleIs(new SingletonLifecycle())
                                           .Use(context => ServiceBusFactory.New(sbc => {
                                                                                        sbc.ReceiveFrom("rabbitmq://localhost/DirectOnTime_TestFrame");
                                                                                        sbc.UseRabbitMq();
                                                                                        sbc.UseRabbitMqRouting();
                                                                                        sbc.Subscribe(subs =>
                                                                                                          {
                                                                                                              subs.LoadFrom(container);
                                                                                                          });

                                                                                    })));

            return container;
        }
    }
}
