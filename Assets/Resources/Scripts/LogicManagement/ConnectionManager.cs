using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager {

    private static ConnectionManager instance = new ConnectionManager();
    private Dictionary<string, Connection> connections;


    public static ConnectionManager getInstance()
    {
        return instance;
    }

    private ConnectionManager()
    {
        connections = new Dictionary<string, Connection>();
    }

    public Connection getConnection(string key)
    {
        if (connections.ContainsKey(key))
            return connections[key];
        else
            return null;
    }

    public Dictionary<string, Connection> getConnections()
    {
        return this.connections;
    }

    public void stopConnection(string id)
    {
        Connection c = this.getConnection(id);
        c.connState = Connection.States.Inactive;
    }

    public void startConnection(string id)
    {
        Connection c = this.getConnection(id);
        c.connState = Connection.States.Active;
    }

    public void deleteConnection(string id)
    {
        Connection c = this.getConnection(id);
        if (c!=null)
        {
            c.connState = Connection.States.Inactive;
            c.connState = Connection.States.Deleted;
            this.connections.Remove(id);    
        }
    }
   
}
