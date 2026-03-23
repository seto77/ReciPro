namespace Crystallography.Controls;

partial class CaptureFormBase
{
    private System.ComponentModel.IContainer components = null;
    protected CaptureExtender captureExtender;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        captureExtender = new CaptureExtender(components);
    }
}
