﻿using AutoMapper;
using Infrastructure.SqlServer.Repositories.SqlServer.DataContext;
using Infrastructure.SqlServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Repositories;
using UseCases.UnitOfWork;

namespace Infrastructure.SqlServer.UnitOfWork
{
    public class SqlServerOrderUnitOfWork : IOrderUnitOfWork
    {
        private readonly BookShopDbContext _context;

        public SqlServerOrderUnitOfWork(BookShopDbContext context, IMapper mapper)
        {
            _context = context;

            BookRepository = new SqlServerBookRepository(context, mapper);
            OrderRepository = new SqlServerOrderRepository(context, mapper);
            OrderItemRepository = new SqlServerOrderItemRepository(context, mapper);
        }

        public IBookRepository BookRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public IOrderItemRepository OrderItemRepository { get; }

        public Task BeginTransactionAsync()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public Task CancelTransactionAsync()
        {
            return _context.Database.RollbackTransactionAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.Database.CommitTransactionAsync();
        }
    }
}
