namespace Trasen.PaperFree.Infrastructure.Database.DbContext;

public class SlaveRoundRobin
{
    //服务器列表
    private readonly IList<string> _items;

    //锁
    private readonly object _syncLock = new object();

    //当前访问的服务器索引，开始是-1，因为没有人访问
    private int _currentIndex = -1;

    public SlaveRoundRobin(IOptions<DbConnectionOption> dbConnection)
    {
        _items = dbConnection.Value.SlaveConnections;
    }

    public string GetNext()
    {
        lock (this._syncLock)
        {
            _currentIndex++;
            //超过数量，索引归0
            if (_currentIndex >= _items.Count)
                _currentIndex = 0;
            return _items[_currentIndex];
        }
    }
}