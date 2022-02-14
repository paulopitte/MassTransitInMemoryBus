using MassTransit;

namespace MassTransitInMemoryBus
{
     static class MassTransitInMemoryService
    {
        public static async void Process()
        {
            // aqui é criado um barramento em memory
            var bus = Bus.Factory.CreateUsingInMemory(sbc =>
            {
                // também é criado um received point que aponta para a fica "order_queue" In-Memory
                sbc.ReceiveEndpoint("order_queue", ep =>
                {
                    // nesse handler é capturado toda mensagem tipada na qual foi publicada na fica em questão,
                    ep.Handler<Message>(context =>
                    {
                        return Console.Out.WriteLineAsync($"Mensagem Recebida: {context.Message.Text}");
                    });
                });
            });

            // aqui é feito o start do barramento, IMPORTANTE É DAR O STOP QUANDO NAO USADO MAIS.
            await bus.StartAsync();

            try
            {
                var index = 0;
                while (true)
                {
                    // Publicação  da devida mensagem
                    object p = bus.Publish(new Message
                    {
                        OrderId = index,
                        Text = $"{DateTime.UtcNow} => messagem publicada com index order: {index++}"
                    });

                    await Task.Run(() => Thread.Sleep(1000));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
            finally
            {
                await bus.StopAsync();
            }
        }


        internal class Message
        {
            public int OrderId { get; set; }
            public string Text { get; set; }
        }

    }
}
