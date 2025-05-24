namespace MediatorVH.Interfaces;

public interface IBaseCommand;
public interface ICommand : IBaseCommand;
public interface ICommand<out TResponse> : IBaseCommand;