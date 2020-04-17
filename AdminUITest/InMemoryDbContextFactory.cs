using AdminUI.Data;
using Microsoft.EntityFrameworkCore;


public class InMemoryDbContextFactory
    {
        public TimesheetContext GetTimesheetContext()
        {
            var options = new DbContextOptionsBuilder<TimesheetContext>()
                        .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                        .Options;
        var dbContext = new TimesheetContext(options);
            return dbContext;
        }
        public LockTableContext GetLockTableContext()
        {
            var options = new DbContextOptionsBuilder<LockTableContext>()
                        .UseInMemoryDatabase(databaseName: "InMemoryArticleDatabase")
                        .Options;
        var dbContext = new LockTableContext(options);
            return dbContext;
        }
    }
