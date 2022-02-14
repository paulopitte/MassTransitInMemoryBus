# MassTransitInMemoryBus
MassTransit InMemory Queue C# .Net Core, implementação de forma simples, 

* Basicamente é criado um Bus mapeando uma área de memoria.
* A idéia simples é abstrair a infra de mensageria para execução de testes Unitario e desenvovimento.
* Importante, não funciona em comunicação "inter-processos", a Thread tem que estar rodando de baixo do principal, ou seja esse processo não atende produção.
