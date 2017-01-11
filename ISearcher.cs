using System;
#if SSHARP
using Crestron.SimplSharp;
using IPAddress = Crestron.SimplSharp.IPAddress;
using IPEndPoint = Crestron.SimplSharp.IPEndPoint;
#else
using System.Net.Sockets;
using System.Net;
#endif
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace SSMono.Nat
	{
	public delegate void NatDeviceCallback (INatDevice device);

	internal interface ISearcher
		{
		event EventHandler<DeviceEventArgs> DeviceFound;
		event EventHandler<DeviceEventArgs> DeviceLost;

		void Search ();
		void Handle (IPAddress localAddress, byte[] response, IPEndPoint endpoint);
		DateTime NextSearch { get; }
		}
	}
