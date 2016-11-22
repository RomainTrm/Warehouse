using Moq;
using NFluent;
using NUnit.Framework;
using Warehouse.Domain.Commands;
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
            Check.ThatCode(() => commandBus.RegsiterHandler((ICommandHandler<CommandFake1>) null))
                .Throws<CommandBusException>();
        }

        [Test]
        public void ThrowsCommandBusExceptionWhenRegisterHandlerForSameCommandType()
        {
            var handler1Mock = new Mock<ICommandHandler<CommandFake1>>();
            var handler2Mock = new Mock<ICommandHandler<CommandFake1>>();

            var commandBus = new CommandBus();
            commandBus.RegsiterHandler(handler1Mock.Object);

            Check.ThatCode(() => commandBus.RegsiterHandler(handler2Mock.Object))
                .Throws<CommandBusException>();
        }

        [Test]
        public void SendCommandToCorrectCommandHandler()
        {
            var handler1Mock = new Mock<ICommandHandler<CommandFake1>>();
            var handler2Mock = new Mock<ICommandHandler<CommandFake2>>();

            var commandBus = new CommandBus();
            commandBus.RegsiterHandler(handler1Mock.Object);
            commandBus.RegsiterHandler(handler2Mock.Object);

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
            commandBus.RegsiterHandler(handler1Mock.Object);

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