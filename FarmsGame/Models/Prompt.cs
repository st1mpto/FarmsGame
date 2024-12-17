using System.Drawing;
using System.Windows.Forms;

public static class Prompt
{
    public static string ShowDialog(string text, string caption)
    {
        var prompt = new Form()
        {
            Width = 400,
            Height = 200,
            Text = caption,
            StartPosition = FormStartPosition.CenterParent
        };

        var textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 350 };
        var textBox = new TextBox() { Left = 20, Top = 50, Width = 350 };
        var confirmation = new Button() { Text = "OK", Left = 150, Width = 100, Top = 100 };

        confirmation.Click += (sender, e) => { prompt.Close(); };
        prompt.Controls.Add(textBox);
        prompt.Controls.Add(confirmation);
        prompt.Controls.Add(textLabel);
        prompt.AcceptButton = confirmation;

        prompt.ShowDialog();
        return textBox.Text;
    }
}
