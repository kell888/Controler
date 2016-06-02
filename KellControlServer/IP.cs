using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace KellControlServer
{
    public class GetIP
    {       
        private IPAddress IPv4, IPv6;

        public GetIP()//构造函数   
        {            
            GetAllIP();
        }        

        private void GetAllIP()   
        {        
            IPAddress [] ipList= Dns.GetHostAddresses(Dns.GetHostName()); 
            foreach (IPAddress ip in ipList)  
            {              
                //获得IPv4  
                if (ip.AddressFamily == AddressFamily.InterNetwork)     
                    IPv4 = ip;               
                //获得IPv6                
                if (ip.AddressFamily == AddressFamily.InterNetworkV6)  
                    IPv6 = ip;       
            }     
        }       
        public IPAddress GetLocalIPv4()//通过这个public函数获取ipv4 
        {         
            try   
            {        
                if (IPv4 != null)   
                    return IPv4;     
                else                  
                    return null;    
            }            
            catch (Exception error)   
            {            
                return null;          
            }     
        }        
        public IPAddress GetLocalIPv6()//通过这个public函数获取ipv6 
        {          
            try      
            {            
                if (IPv6 != null) 
                    
                    return IPv6;     
                else                  
                    return null;      
            }          
            catch (Exception error)    
            {               
                return null; 
            }      
        }   
    }
}
