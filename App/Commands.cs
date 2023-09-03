namespace SerialPlotter
{
    enum Commands : byte
    {
        SetDeviceSendDataStatus = 0x40,
        AdcReadValue = 0x51
    }
}