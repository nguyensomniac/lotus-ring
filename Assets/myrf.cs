using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
#if NETFX_CORE
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Rfcomm;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Networking.Proximity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;

public class PairedDeviceInfo
{

    internal PairedDeviceInfo(PeerInformation peerInformation)
    {
        this.PeerInfo = peerInformation;
        this.DisplayName = this.PeerInfo.DisplayName;
        this.HostName = this.PeerInfo.HostName.DisplayName;
        this.ServiceName = this.PeerInfo.ServiceName;
    }
    public string DisplayName { get; private set; }
    public string HostName { get; private set; }
    public string ServiceName { get; private set; }
    public PeerInformation PeerInfo { get; private set; }
}
#endif


public class myrf : MonoBehaviour
{
#if NETFX_CORE
    private Windows.Devices.Bluetooth.Rfcomm.RfcommDeviceService _service;
    private StreamSocket _socket;
    private DataWriter dataWriterObject;
    private DataReader dataReaderObject;
    ObservableCollection<DeviceInformation> _pairedDevices;
    private CancellationTokenSource ReadCancellationTokenSource;

#endif

    //public string deviceName = "HC-06";
    private string deviceName = "HC-06";
    private string stringbuffer;
    //private string deviceName = "FT232R USB UART";

    void Start()
    {
        Application.logMessageReceived += LogMessage;
        stringbuffer = String.Empty;
#if NETFX_CORE
        Init();
#endif
    }

    void Update()
    {

    }

#if NETFX_CORE
    private async void Init()
    {
        try
        {
            DeviceInformationCollection DeviceInfoCollection = await DeviceInformation.FindAllAsync(RfcommDeviceService.GetDeviceSelector(RfcommServiceId.SerialPort));

            //var numDevices = DeviceInfoCollection.Length;
            //Debug.LogFormat("{0} devices found!", numDevices);

            // By clearing the backing data, we are effectively clearing the ListBox
            _pairedDevices = new ObservableCollection<DeviceInformation>();
            _pairedDevices.Clear();

            //if (numDevices == 0)
            //{
            //    Debug.Log("No paired devices found");
            //    System.Diagnostics.Debug.WriteLine("InitializeRfcommDeviceService: No paired devices found.");
            //}
            //else
            //{
                // Found paired devices.
                foreach (var deviceInfo in DeviceInfoCollection)
                {
                    Debug.LogFormat("Found paired device: {0}", deviceInfo.Id);
                    //if (deviceInfo.Id.Contains("f8:63:3f:4a:3e:da")) {
                        ConnectToDevice(deviceInfo);
                    //}
                    _pairedDevices.Add(deviceInfo);
                }

            //}
        }
        catch (Exception ex)
        {
            Debug.LogFormat("Init exception: {0}" , ex.Message);
        }
    }

    private async void ConnectToDevice(DeviceInformation deviceInfo)
    {
        //Debug.Log("Time to connect to device!");
        bool success = true;
        try {
            //Debug.Log("help meeeee");
            //Debug.Log(deviceInfo.Id);
            _service = await RfcommDeviceService.FromIdAsync(deviceInfo.Id);
            //Debug.Log("Found service");
            if (_socket != null) {
                // Disposing the socket with close it and release all resources associated with the socket
                _socket.Dispose();
            }

            _socket = new StreamSocket();
            try {
                Debug.Log("attemping async connect");
                // Note: If either parameter is null or empty, the call will throw an exception
                await _socket.ConnectAsync(_service.ConnectionHostName, _service.ConnectionServiceName);
            } catch (Exception ex) {
                success = false;
                Debug.Log("Connect:" + ex.Message);
            }
            // If the connection was successful, the RemoteAddress field will be populated
            if (success) {
                string msg = String.Format("Connected to {0}!", _socket.Information.RemoteAddress.DisplayName);
                //MessageDialog md = new MessageDialog(msg, Title);
                Debug.Log(msg);
                //await md.ShowAsync();
                Listen();
            }
        } catch (Exception ex) {
            Debug.Log("Overall Connect: " + ex.Message);
            _socket.Dispose();
            _socket = null;
        }
    }

    /// <summary>
    /// - Create a DataReader object
    /// - Create an async task to read from the SerialDevice InputStream
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Listen()
    {
        Debug.Log("Start listening to stream!");
        try {
            ReadCancellationTokenSource = new CancellationTokenSource();
            if (_socket.InputStream != null) {
                dataReaderObject = new DataReader(_socket.InputStream);
                // keep reading the serial input
                while (true) {
                    await ReadAsync(ReadCancellationTokenSource.Token);
                }
            }
        } catch (Exception ex) {
            if (ex.GetType().Name == "TaskCanceledException") {
                System.Diagnostics.Debug.WriteLine("Listen: Reading task was cancelled, closing device and cleaning up");
            } else {
                System.Diagnostics.Debug.WriteLine("Listen: " + ex.Message);
            }
        } finally {
            // Cleanup once complete
            if (dataReaderObject != null) {
                dataReaderObject.DetachStream();
                dataReaderObject = null;
            }
        }
    }


    /// <summary>
    /// ReadAsync: Task that waits on data and reads asynchronously from the serial device InputStream
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task ReadAsync(CancellationToken cancellationToken)
    {
        Task<UInt32> loadAsyncTask;

        uint ReadBufferLength = 1024;

        // If task cancellation was requested, comply
        cancellationToken.ThrowIfCancellationRequested();

        // Set InputStreamOptions to complete the asynchronous read operation when one or more bytes is available
        dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

        // Create a task object to wait for data on the serialPort.InputStream
        loadAsyncTask = dataReaderObject.LoadAsync(ReadBufferLength).AsTask(cancellationToken);

        // Launch the task and wait
        UInt32 bytesRead = await loadAsyncTask;
        if (bytesRead > 0) {
            try {
                string received = dataReaderObject.ReadString(bytesRead);
                stringbuffer += received;
                int delimIdx = stringbuffer.IndexOf("\n");
                if (delimIdx != -1) {
                    string cmd = stringbuffer.Substring(0, delimIdx); // Sans delimiter
                    stringbuffer = stringbuffer.Substring(delimIdx + 1);
                    // Remove amt of string including delimiter.
                    Debug.LogFormat("{0} ", cmd);
                    switch (cmd) {
                        case "u":
                            KeyboardEventManager.TriggerEvent("TopSwipe", 0);
                            break;
                        case "d":
                            KeyboardEventManager.TriggerEvent("BottomSwipe", 0);
                            break;
                        case "l":
                            KeyboardEventManager.TriggerEvent("LeftSwipe", 0);
                            break;
                        case "r":
                            KeyboardEventManager.TriggerEvent("RightSwipe", 0);
                            break;
                        case "h":
                            KeyboardEventManager.TriggerEvent("CenterTap", 0);
                            break;
                        case "n":
                            KeyboardEventManager.TriggerEvent("CenterTapRelease", 0);
                            break;
                        default:
                            // Numbers! (scroll)
                            KeyboardEventManager.TriggerEvent("ScrollDown", -5);
                            int num = 0;
                            if (Int32.TryParse(cmd, out num)) {
                                if (num < 0) {
                                    // Negative number is clockwise
                                    KeyboardEventManager.TriggerEvent("ScrollDown", num);
                                } else {
                                    KeyboardEventManager.TriggerEvent("ScrollUp", num);
                                }
                            } else {
                                Debug.LogFormat("Command input `{0}` is invalid or not a number.");
                            }
                            break;
                    }
                }
                //Debug.LogFormat("Received text: {0}", received);
            } catch (Exception ex) {
                Debug.Log("ReadAsync: " + ex.Message);
            }

        }
    }
#endif

    public void LogMessage(string message, string stackTrace, LogType type)
    {
    }
}
