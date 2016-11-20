using System;

namespace Warehouse.DataAccess.Events
{
    internal interface IDateTimeProvider
    {
        DateTime GetDateTime();
    }
}