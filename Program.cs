using System.Collections.Generic;
using System.Windows.Forms;

namespace DemoButtons
{
  static class Program
  {
    [System.STAThread]
    static void Main()
    {
      System.Windows.Forms.Application.EnableVisualStyles();
      System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //System.Windows.Forms.Application.Run(new DemoButtonsForm());
            GetSignature();
            Application.Exit();
     }

        public static void GetSignature()
        {
            int penDataType;
            List<wgssSTU.IPenDataTimeCountSequence> penTimeData = null;
            List<wgssSTU.IPenData> penData = null;

            wgssSTU.UsbDevices usbDevices = new wgssSTU.UsbDevices();
            if (usbDevices.Count != 0)
            {
                try
                {
                    wgssSTU.IUsbDevice usbDevice = usbDevices[0]; // select a device

                    SignatureForm demo = new SignatureForm(usbDevice, false);
                    demo.Text = "Salguero - Firma Báscula";
                    demo.ShowDialog();
                    penDataType = demo.penDataType;

                    if (penDataType == (int)PenDataOptionMode.PenDataOptionMode_TimeCountSequence)
                        penTimeData = demo.getPenTimeData();
                    else
                        penData = demo.getPenData();

                    if (penData != null || penTimeData != null)
                    {
                        // process penData here!
                        /*
                        if (penData != null)
                            txtPenDataCount.Text = penData.Count.ToString();
                        else
                            txtPenDataCount.Text = penTimeData.Count.ToString();
                        */
                        wgssSTU.IInformation information = demo.getInformation();
                        wgssSTU.ICapability capability = demo.getCapability();
                    }
                    demo.Dispose();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No STU devices attached");
            }
        }
    }
}
