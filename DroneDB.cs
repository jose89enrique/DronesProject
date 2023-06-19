namespace Drones.DB;
using System.Linq;


public enum Model {Lightweight, Middleweight, Cruiserweight, Heavyweight};

public enum State {IDLE, LOADING, LOADED, DELIVERING, DELIVERED, RETURNING};

public record Drone
{
    public string? SerialNumber {get; set;} //1000 characters max
    public Model model {get; set;}
    public int weight {get; set;} //500gr max
    public int batteryCapacity {get; set;} //[0-100]
    public State state{get; set;}  
    public List<Medication>? load{get; set;}  
}

public record Medication
{
    public string ? name {get; set;}
    public int weight{get; set;}
    public string ? code{get; set;}
    public string ? image{get; set;}
}

public record LoadDrone
{
    public string? SerialNumber{get; set;}
    public List<Medication>? load{get; set;}
}

public record RegisterDrone
{
    public string? SerialNumber {get; set;} //1000 characters max
    public Model model {get; set;}
    public int weight {get; set;} //500gr max    
}

public class DroneDB
{
    private static List<Drone> _drones= new List<Drone>()
    {
        new Drone{ SerialNumber = "001", model=Model.Lightweight, weight=100, batteryCapacity=100, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "002", model=Model.Lightweight, weight=100, batteryCapacity=90, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "003", model=Model.Lightweight, weight=100, batteryCapacity=80, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "004", model=Model.Lightweight, weight=100, batteryCapacity=70, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "005", model=Model.Lightweight, weight=100, batteryCapacity=90, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "006", model=Model.Lightweight, weight=100, batteryCapacity=80, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "007", model=Model.Lightweight, weight=100, batteryCapacity=70, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "008", model=Model.Lightweight, weight=100, batteryCapacity=60, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "009", model=Model.Lightweight, weight=100, batteryCapacity=70, state=State.IDLE, load=new List<Medication>()},
        new Drone{ SerialNumber = "010", model=Model.Lightweight, weight=100, batteryCapacity=90, state=State.IDLE, load=new List<Medication>()}
    
    };
    
    
    //Registering a drone
    public static object registerDrone(RegisterDrone newDrone)
    {    
        
        return null;     
    }

    //Loading a drone with medication items
    public static object loadMedication(LoadDrone input)
    {
        
        return null;
    }

    //Checking loaded medication items for a given drone
    public static object checkLoadDrone(string droneSN)
    {
        return null;
             
    }

    //Checking avaible drones for loading
    public static List<Drone> checkDronesReady()
    {
        return null;
    }

    //Check drone battery level for a given drone 
    public static object checkBatteryDrone(string serial)
    {
        return null;
    }

    
    
} 