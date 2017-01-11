using System;
#if SSHARP
using Crestron.SimplSharp;
using IAsyncResult = Crestron.SimplSharp.CrestronIO.IAsyncResult;
using AsyncCallback = Crestron.SimplSharp.CrestronIO.AsyncCallback;
using WaitHandle = Crestron.SimplSharp.CEventHandle;
#else
#endif
using System.Collections.Generic;
using System.Text;

namespace SSMono.Nat
	{
	internal class AsyncResult : IAsyncResult
		{
		private object asyncState;
		private AsyncCallback callback;
		private bool completedSynchronously;
		private bool isCompleted;
		private Exception storedException;
		private ManualResetEvent waitHandle;

		public AsyncResult (AsyncCallback callback, object asyncState)
			{
			this.callback = callback;
			this.asyncState = asyncState;
			waitHandle = new ManualResetEvent (false);
			}

		public object AsyncState
			{
			get { return asyncState; }
			}

		public ManualResetEvent AsyncWaitHandle
			{
			get { return waitHandle; }
			}

		WaitHandle IAsyncResult.AsyncWaitHandle
			{
			get { return waitHandle; }
			}

		public bool CompletedSynchronously
			{
			get { return completedSynchronously; }
			protected internal set { completedSynchronously = value; }
			}

		public bool IsCompleted
			{
			get { return isCompleted; }
			protected internal set { isCompleted = value; }
			}

		public Exception StoredException
			{
			get { return storedException; }
			}

		public void Complete ()
			{
			Complete (storedException);
			}

		public void Complete (Exception ex)
			{
			storedException = ex;
			isCompleted = true;
			waitHandle.Set ();

			if (callback != null)
				callback (this);
			}

#if SSHARP
		#region IAsyncResult Members

		public object InnerObject
			{
			get { throw new NotImplementedException (); }
			}

		#endregion
#endif
		}
	}
