using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.SlaveDbSelector
{
    public class SlaveDbSelector : ISlaveDbSelector
    {
        private readonly List<string> _slaveConnectString;
        private int _currentIndex = 0;

        public SlaveDbSelector(IConfiguration configuration)
        {
            _slaveConnectString = configuration.GetSection("ConnectionStrings:Slave").GetChildren()
                .Select(x => x.Value 
                            ?? throw new Exception("Have errors with get connection string in slave db")).ToList() 
                                  ?? throw new Exception("Not setup connection string for slave db key get is ConnectionStrings:SlaveDB");
        }
        // xoay vong tron cho den het slave db
        public string GetConnectionString()
        {
            var connectString = _slaveConnectString.ElementAt(_currentIndex);
            _currentIndex = (_currentIndex + 1) % _slaveConnectString.Count;
            return connectString;
        }
        
    }
}