using ControlFlow.Core.Interfaces.IRepositories;
using ControlFlow.Core.Interfaces.IUnitOfWork;
using ControlFlow.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ControlFlow.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly IdentityAppContext _appContext;

    public UnitOfWork(IdentityAppContext appContext)
    {
        _appContext = appContext;
        ApplicationUserRepo = new ApplicationUserTaskRepo(appContext);
        UserTaskRepo = new UserTaskRepo(appContext);
    }

    public IApplicationUserRepo ApplicationUserRepo { get; set; }
    public IUserTaskRepo UserTaskRepo { get; set; }

    public void Dispose()
    {
        _appContext.Dispose();  
    }

    public int SaveChanges()
    {
        return _appContext.SaveChanges();
    }
}
