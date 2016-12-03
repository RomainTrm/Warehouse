using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Commands.Base;
using Warehouse.Domain.Commands.Bus;
using Warehouse.Domain.Commands.Exceptions;

namespace Warehouse.Domain.Tests.Commands
{
    [TestFixture]
    public class CommandBusShould
    {
        [Test]
        public void ThrowsCommandBusExceptionWhenRegisterNullHandler()
        {
            var commandBus = new CommandBus();
            Check.ThatCode(() => commandBus.RegisterHandler((ICommandHandler<CommandFake1>) null))
                .Throws<CommandBusException>();
        }

        [Test]
        public void ThrowsCommandBusExceptionWhenRegisterHandlerForSameCommandType()
        {
            var handler1Mock = new Mock<ICommandHandler<CommandFake1>>();
            var handler2Mock = new Mock<ICommandHandler<CommandFake1>>();

            var commandBus = new CommandBus();
            commandBus.RegisterHandler(handler1Mock.Object);

            Check.ThatCode(() => commandBus.RegisterHandler(handler2Mock.Object))
                .Throws<CommandBusException>();
        }

        [Test]
        public void SendCommandToCorrectCommandHandler()
        {
            var handler1Mock = new Mock<ICommandHandler<CommandFake1>>();
            var handler2Mock = new Mock<ICommandHandler<CommandFake2>>();

            var commandBus = new CommandBus();
            commandBus.RegisterHandler(handler1Mock.Object);
            commandBus.RegisterHandler(handler2Mock.Object);

            commandBus.Send(new CommandFake1());

            handler1Mock.Verify(x => x.Handle(It.IsAny<CommandFake1>()), Times.Once);
            handler2Mock.Verify(x => x.Handle(It.IsAny<CommandFake2>()), Times.Never);
        }

        [Test]
        public void ThrowCommandBusExceptionWhenSendCommandWithoutHandler()
        {
            var commandBus = new CommandBus();
            Check.ThatCode(() => commandBus.Send(new CommandFake1())).Throws<CommandBusException>();
        }

        [Test]
        public void ThrowCommandHandlersExceptionWhenHandlerThrowsException()
        {
            var handler1Mock = new Mock<ICommandHandler<CommandFake1>>();
            handler1Mock.Setup(x => x.Handle(It.IsAny<CommandFake1>())).Throws(new CommandHandlerException("Error"));

            var commandBus = new CommandBus();
            commandBus.RegisterHandler(handler1Mock.Object);

            Check.ThatCode(() => commandBus.Send(new CommandFake1())).Throws<CommandHandlerException>();
        }
    }

    public class CommandFake1 : ICommand
    {
    }

    public class CommandFake2 : ICommand
    {
    }
}