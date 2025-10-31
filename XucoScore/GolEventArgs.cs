using System;

public class GolEventArgs : EventArgs
{
    public string time { get; set; } = string.Empty;
    public string adversario { get; set; } = string.Empty;
    public string jogador { get; set; } = string.Empty;
}