using System.Collections.Generic;

namespace Sales.Server
{
    /// <summary>
    ///  This can be any database technology, it can differ between business components
    /// </summary>
    public static class Database
    {
        private static int _id = 0;

        public static string SaveOrder(IEnumerable<string> productIds, string userId, string shippingTypeId)
        {
            var nextOrderId = _id++;
            return nextOrderId.ToString();
        }
    }
}