namespace Drones.DB;
using System.Linq;
using System.Threading;

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
    
    private static Timer? timer=null;

    //Registering a drone
    public static object registerDrone(RegisterDrone newDrone)
    {    
        Drone? result;
        if(newDrone.SerialNumber!=null && newDrone.SerialNumber.Length>1000)
            return new {error="Wrong serial number; the limit is 1000 characters"};  
        else
        {
            int count = (from dr in _drones where dr.SerialNumber==newDrone.SerialNumber select dr).Count();
            if(count>0)
                return new {error = "The serial number is wrong. Exist another registered drone with this number."};
        }

        if(newDrone.weight<0 || newDrone.weight>500)
            return new {error="The weight is over the limit! It has to be positive and under 500gr"};      
        else
        {
            result=new Drone(){SerialNumber= newDrone.SerialNumber, model=newDrone.model, weight=newDrone.weight, state=State.IDLE, batteryCapacity=100, load=new List<Medication>()};
            _drones.Add(result);
            return result;  
        } 
    }

    //Loading a drone with medication items
    public static object loadMedication(LoadDrone input)
    {
        string chars= "abcdefghijklmnopqrstuvwxyz";
        string numbers="0123456789";
        Drone? droneM= _drones.SingleOrDefault(dr => dr.SerialNumber == input.SerialNumber);
        if(droneM != default(Drone))
        {            
            int currentLoad= (from dr in droneM.load select dr.weight).Sum();
            droneM.state=State.LOADING;
            if(input.load!=null)
            {
                foreach (Medication item in input.load)
                {
                    //Only letters and numbers, -, _ in the name
                    if(item.name!=null)
                    {
                        for (int i=0; i<item.name.Length;i++)
                        {
                            if(!(chars+chars.ToUpper()+numbers+"-_").Contains(item.name[i]))
                                return new {error= string.Format("The name {0} is wrong for a medication. Just be allowed letters, numbers, - and underscore.", item.name)};                            
                        }
                    }
                    else return new {error= string.Format("The name {0} is wrong for a medication. Just be allowed letters, numbers, - and underscore.", item.name)};
                    
                    //Code: allowed only upper case letters, underscore and numbers
                    if(item.code!=null)
                    {
                        for (int i=0; i<item.code.Length;i++)
                        {
                            if(!(chars.ToUpper()+numbers+"_").Contains(item.code[i]))
                                return new {error= string.Format("The code {0} is wrong for a medication. Just be allowed letters, numbers and underscore.", item.code)};                            
                        }
                    }
                    else return new {error= string.Format("The code {0} is wrong for a medication. Just be allowed letters, numbers and underscore.", item.code)};
                    
                    if(currentLoad+item.weight>droneM.weight)
                        return new {error= "The Medications exceed load limits."};
                    else
                    {
                        currentLoad+=item.weight;
                        if(droneM.load==null)
                            droneM.load=new List<Medication>(){item};
                        else
                            droneM.load.Add(item);
                    }
                }
            }
            droneM.state=State.LOADED;

            if(droneM.batteryCapacity<25)
                return new {warning= "The battery level is below 25 percent"};
            else
            {
                return droneM;
            }
        }
        else
        {
            return new {error= String.Format("The drone {0} is not registered", input.SerialNumber)};
        }
    }

    //Checking loaded medication items for a given drone
    public static object checkLoadDrone(string droneSN)
    {
       if(droneSN == default(string))
            return new {error= "Empty serial number!"};
        else
        {
            Drone? droneM= _drones.SingleOrDefault(dr => dr.SerialNumber == droneSN);
           // List<Drone> result= (from dr in _drones where dr.SerialNumber==droneSN select dr).ToList();
            if(droneM == default(Drone))
                return new {error=String.Format("Don't exist any drone with the serial number {0}", droneSN)};
            else
            {                         
                if(droneM.load!=null)                       
                    return droneM.load;
                else
                    return new List<Drone>(); 
            }
        }
             
    }

    //Checking avaible drones for loading
    public static List<Drone> checkDronesReady()
    {
        List<Drone> result=(from dr in _drones where dr.state==State.IDLE select dr).ToList();

        return result;
    }

    //Check drone battery level for a given drone 
    public static object checkBatteryDrone(string serial)
    {
         Drone? droneM= _drones.SingleOrDefault(dr => dr.SerialNumber == serial);
        //List<Drone> result=(from dr in _drones where dr.state==State.IDLE select dr).ToList();
        if(droneM == default(Drone))
            return new {error=String.Format("Don't exist any drone with the serial number {0}", serial)};
        else            
            return droneM.batteryCapacity;
    }

    //Start checking system for the battery level
    public static void startTime()
    {
        timer=new Timer(new TimerCallback(checkBattery),null, 1000, 20000);        
    }

    public static void checkBattery(object? state)
    {
        Console.ForegroundColor= ConsoleColor.Yellow;        
        Console.WriteLine("Report battery =>");
        Console.ResetColor();
        for(int i=0; i<_drones.Count(); i++)
        {
            if(_drones[i].batteryCapacity>=10)
                _drones[i].batteryCapacity-=10;
            else
                _drones[i].batteryCapacity=0;
             
             Console.WriteLine(String.Format("Drone {0}: {1} %", _drones[i].SerialNumber, _drones[i].batteryCapacity));
        }

        Console.Beep();
    }
} 