using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess___
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "White's turn";

            int broj = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox a = new PictureBox();

                    if ((i + j) % 2 == 0) { a.BackColor = Color.Beige; a.WaitOnLoad = true; }
                    if ((i + j) % 2 == 1) { a.BackColor = Color.RoyalBlue; a.WaitOnLoad = false; }

                    a.Width = 60;
                    a.Height = 60;

                    a.Location = new Point((i + 1) * 60, (j + 1) * 60);

                    if (i == 0 && j == 0) { a.Tag = "br"; }
                    if (i == 1 && j == 0) { a.Tag = "bn"; }
                    if (i == 2 && j == 0) { a.Tag = "bb"; }
                    if (i == 3 && j == 0) { a.Tag = "bq"; }
                    if (i == 4 && j == 0) { a.Tag = "bk"; }
                    if (i == 5 && j == 0) { a.Tag = "bb"; }
                    if (i == 6 && j == 0) { a.Tag = "bn"; }
                    if (i == 7 && j == 0) { a.Tag = "br"; }
                    if (j == 1) { a.Tag = "bp"; }

                    if (j == 6) { a.Tag = "wp"; }
                    if (i == 0 && j == 7) { a.Tag = "wr"; }
                    if (i == 1 && j == 7) { a.Tag = "wn"; }
                    if (i == 2 && j == 7) { a.Tag = "wb"; }
                    if (i == 3 && j == 7) { a.Tag = "wq"; }
                    if (i == 4 && j == 7) { a.Tag = "wk"; }
                    if (i == 5 && j == 7) { a.Tag = "wb"; }
                    if (i == 6 && j == 7) { a.Tag = "wn"; }
                    if (i == 7 && j == 7) { a.Tag = "wr"; }

                    a.Click += click;

                    a.Name = broj.ToString();
                    Controls.Add(a);
                    broj++;
                }
            }

            //buttons i panel
            {
                Button buttonq = new Button();
                buttonq.FlatStyle = FlatStyle.Flat;
                buttonq.Location = new Point(0, 0);
                buttonq.Width = 120;
                buttonq.Height = 60;
                buttonq.Name = "q";
                buttonq.Text = "Queen";
                buttonq.Click += peon_button_Click;

                Button buttonr = new Button();
                buttonr.FlatStyle = FlatStyle.Flat;
                buttonr.Location = new Point(0, 60);
                buttonr.Width = 120;
                buttonr.Height = 60;
                buttonr.Name = "r";
                buttonr.Text = "Rook";
                buttonr.Click += peon_button_Click;

                Button buttonb = new Button();
                buttonb.FlatStyle = FlatStyle.Flat;
                buttonb.Location = new Point(0, 120);
                buttonb.Width = 120;
                buttonb.Height = 60;
                buttonb.Name = "b";
                buttonb.Text = "Bishop";
                buttonb.Click += peon_button_Click;

                Button buttonn = new Button();
                buttonn.FlatStyle = FlatStyle.Flat;
                buttonn.Location = new Point(0, 180);
                buttonn.Width = 120;
                buttonn.Height = 60;
                buttonn.Name = "n";
                buttonn.Text = "Knight";
                buttonn.Click += peon_button_Click;

                Panel panel = new Panel();
                panel.Location = new Point(540, 300);
                panel.Width = 120;
                panel.Height = 240;
                panel.BackColor = Color.Beige;
                panel.Controls.Add(buttonq);
                panel.Controls.Add(buttonn);
                panel.Controls.Add(buttonr);
                panel.Controls.Add(buttonb);
                panel.Visible = false;
                Controls.Add(panel);
            }

            update_pic();
        }
        private void peon_button_Click(object sender, EventArgs e)
        {
            //Controls.Remove((Button)sender);    
            //ss((Button)sender);
            peon_button2((Button)sender);
        }
        public void peon_button2(Button btn)
        {
            //aa.BackColor = Color.Red;
            //aa.Parent.Controls.Remove(aa);
            transform_to = btn.Name;
            foreach (Panel item in Controls.OfType<Panel>())
            {
                item.Visible = false;
                foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                {
                    if (item2.Name == transform_this)
                    {
                        if (!white_turn) item2.Tag = "w" + transform_to;
                        if (white_turn) item2.Tag = "b" + transform_to;

                        update_pic();
                    }
                    item2.Enabled = true;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            recolor();
        }
        private void click(object sender, EventArgs e)
        {
            //debug_tag_reci((PictureBox)sender);
            main((PictureBox)sender);
        }

        public void all_pieces(PictureBox clicked_box, string w_or_b, bool extra)
        {
            string temp = "";
            string temp2 = "";
            int temp_num = 0;
            int temp_num_for_pawn = 0;
            if (w_or_b == "w")
            {
                temp = "w";
                temp2 = "b";
                temp_num = 60;
                temp_num_for_pawn = 300;
            }
            if (w_or_b == "b")
            {
                temp = "b";
                temp2 = "w";
                temp_num = -60;
                temp_num_for_pawn = 240;
            }
            if (clicked_box.Tag != null)
            {
                if (clicked_box.Tag.ToString() == temp + "p")
                {
                    if (extra)
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                    }
                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + temp_num && item.Tag == null) item.BackColor = Color.Lime;
                        if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + (2 * temp_num) && item.Location.Y == temp_num_for_pawn && item.Tag == null) item.BackColor = Color.Lime;
                        if (clicked_box.Location.X == item.Location.X + temp_num && clicked_box.Location.Y == item.Location.Y + temp_num && item.Tag != null && item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        if (clicked_box.Location.X == item.Location.X - temp_num && clicked_box.Location.Y == item.Location.Y + temp_num && item.Tag != null && item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                    }
                }
                if (clicked_box.Tag.ToString() == temp + "k")
                {

                    if (extra)
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                    }

                    //goto mark1;
                    if (floating_piece.Tag != null && floating_piece.Tag.ToString().Contains("k"))
                    {
                        if (!whitechecked && !wkmoved && w_or_b == "w")
                        {
                            bool condition1 = false;
                            bool condition2 = false;
                            bool condition3 = false;
                            bool condition4 = false;
                            bool condition5 = false;
                            bool condition6 = false;
                            bool condition7 = false;
                            foreach (PictureBox clicked_box3 in Controls.OfType<PictureBox>())
                            {
                                #region bm

                                if (clicked_box3.Tag == "bp")
                                {
                                    foreach (PictureBox clicked_box4 in Controls.OfType<PictureBox>())
                                    {
                                        if (clicked_box3.Location.X == clicked_box4.Location.X && clicked_box3.Location.Y == clicked_box4.Location.Y - 60 && clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                        if (clicked_box3.Location.X == clicked_box4.Location.X && clicked_box3.Location.Y == clicked_box4.Location.Y - 120 && clicked_box4.Location.Y == 240 && clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 60 && clicked_box3.Location.Y == clicked_box4.Location.Y - 60 && clicked_box4.Tag != null && clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 60 && clicked_box3.Location.Y == clicked_box4.Location.Y - 60 && clicked_box4.Tag != null && clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                    }
                                }
                                if (clicked_box3.Tag == "bk")
                                {
                                    foreach (PictureBox clicked_box4 in Controls.OfType<PictureBox>())
                                    {
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 60 && clicked_box3.Location.Y == clicked_box4.Location.Y + 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 60 && clicked_box3.Location.Y == clicked_box4.Location.Y - 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 60 && clicked_box3.Location.Y == clicked_box4.Location.Y + 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 60 && clicked_box3.Location.Y == clicked_box4.Location.Y - 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 60 && clicked_box3.Location.Y == clicked_box4.Location.Y)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 60 && clicked_box3.Location.Y == clicked_box4.Location.Y)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X && clicked_box3.Location.Y == clicked_box4.Location.Y + 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X && clicked_box3.Location.Y == clicked_box4.Location.Y - 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                    }
                                }
                                if (clicked_box3.Tag == "bn")
                                {
                                    foreach (PictureBox clicked_box4 in Controls.OfType<PictureBox>())
                                    {
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 60 && clicked_box3.Location.Y == clicked_box4.Location.Y + 120)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 60 && clicked_box3.Location.Y == clicked_box4.Location.Y - 120)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 60 && clicked_box3.Location.Y == clicked_box4.Location.Y + 120)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 60 && clicked_box3.Location.Y == clicked_box4.Location.Y - 120)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 120 && clicked_box3.Location.Y == clicked_box4.Location.Y + 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X + 120 && clicked_box3.Location.Y == clicked_box4.Location.Y - 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 120 && clicked_box3.Location.Y == clicked_box4.Location.Y + 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box4.Location.X - 120 && clicked_box3.Location.Y == clicked_box4.Location.Y - 60)
                                        {
                                            if (clicked_box4.Tag == null) clicked_box4.BackColor = Color.Lime;
                                            else if (clicked_box4.Tag.ToString().Remove(1) == "w") clicked_box4.BackColor = Color.Red;
                                        }
                                    }
                                }
                                if (clicked_box3.Tag == "br")
                                {
                                    inf_move_piece(clicked_box3, 0, 60, "w");
                                    inf_move_piece(clicked_box3, 0, -60, "w");
                                    inf_move_piece(clicked_box3, 60, 0, "w");
                                    inf_move_piece(clicked_box3, -60, 0, "w");
                                }
                                if (clicked_box3.Tag == "bb")
                                {
                                    inf_move_piece(clicked_box3, 60, 60, "w");
                                    inf_move_piece(clicked_box3, -60, -60, "w");
                                    inf_move_piece(clicked_box3, 60, -60, "w");
                                    inf_move_piece(clicked_box3, -60, 60, "w");
                                }
                                if (clicked_box3.Tag == "bq")
                                {
                                    inf_move_piece(clicked_box3, 0, 60, "w");
                                    inf_move_piece(clicked_box3, 0, -60, "w");
                                    inf_move_piece(clicked_box3, 60, 0, "w");
                                    inf_move_piece(clicked_box3, -60, 0, "w");
                                    inf_move_piece(clicked_box3, 60, 60, "w");
                                    inf_move_piece(clicked_box3, -60, -60, "w");
                                    inf_move_piece(clicked_box3, 60, -60, "w");
                                    inf_move_piece(clicked_box3, -60, 60, "w");
                                }


                                #endregion
                            }
                            if (clicked_box.Location.X == 300 && clicked_box.Location.Y == 480)
                            {
                                foreach (PictureBox clicked_box2 in Controls.OfType<PictureBox>())
                                {
                                    if (!rwrmoved)
                                    {
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 360 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition1 = true; }
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 420 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition2 = true; ; }
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 480 && clicked_box2.Tag == "wr") { condition3 = true; }
                                    }
                                    if (!lwrmoved)
                                    {
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 240 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition4 = true; }
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 180 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition5 = true; }
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 120 && clicked_box2.Tag == null) { condition6 = true; }
                                        if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 60 && clicked_box2.Tag == "wr") { condition7 = true; }
                                    }
                                }
                            }
                            recolor();
                            if ((condition1 && condition2 && condition3) || (condition4 && condition5 && condition6 && condition7))
                            {
                                foreach (PictureBox clicked_box2 in Controls.OfType<PictureBox>())
                                {
                                    if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 420 && (condition1 && condition2 && condition3))
                                    {
                                        clicked_box2.BackColor = Color.Magenta;
                                    }
                                    if (clicked_box2.Location.Y == 480 && clicked_box2.Location.X == 180 && (condition4 && condition5 && condition6 && condition7))
                                    {
                                        clicked_box2.BackColor = Color.Magenta;
                                    }
                                }
                            }
                        }
                        if (!blackchecked && !bkmoved && w_or_b == "b")
                        {
                            bool condition1 = false;
                            bool condition2 = false;
                            bool condition3 = false;
                            bool condition4 = false;
                            bool condition5 = false;
                            bool condition6 = false;
                            bool condition7 = false;
                            foreach (PictureBox clicked_box3 in Controls.OfType<PictureBox>())
                            {
                                #region wm

                                if (clicked_box3.Tag == "wp")
                                {
                                    foreach (PictureBox clicked_box34 in Controls.OfType<PictureBox>())
                                    {
                                        if (clicked_box3.Location.X == clicked_box34.Location.X && clicked_box3.Location.Y == clicked_box34.Location.Y + 60 && clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                        if (clicked_box3.Location.X == clicked_box34.Location.X && clicked_box3.Location.Y == clicked_box34.Location.Y + 120 && clicked_box34.Location.Y == 300 && clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 60 && clicked_box3.Location.Y == clicked_box34.Location.Y + 60 && clicked_box34.Tag != null && clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 60 && clicked_box3.Location.Y == clicked_box34.Location.Y + 60 && clicked_box34.Tag != null && clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                    }
                                }
                                if (clicked_box3.Tag == "wk")
                                {
                                    foreach (PictureBox clicked_box34 in Controls.OfType<PictureBox>())
                                    {
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 60 && clicked_box3.Location.Y == clicked_box34.Location.Y + 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 60 && clicked_box3.Location.Y == clicked_box34.Location.Y - 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 60 && clicked_box3.Location.Y == clicked_box34.Location.Y + 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 60 && clicked_box3.Location.Y == clicked_box34.Location.Y - 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 60 && clicked_box3.Location.Y == clicked_box34.Location.Y)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 60 && clicked_box3.Location.Y == clicked_box34.Location.Y)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X && clicked_box3.Location.Y == clicked_box34.Location.Y + 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X && clicked_box3.Location.Y == clicked_box34.Location.Y - 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                    }
                                }
                                if (clicked_box3.Tag == "wn")
                                {
                                    foreach (PictureBox clicked_box34 in Controls.OfType<PictureBox>())
                                    {
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 60 && clicked_box3.Location.Y == clicked_box34.Location.Y + 120)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 60 && clicked_box3.Location.Y == clicked_box34.Location.Y - 120)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 60 && clicked_box3.Location.Y == clicked_box34.Location.Y + 120)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 60 && clicked_box3.Location.Y == clicked_box34.Location.Y - 120)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 120 && clicked_box3.Location.Y == clicked_box34.Location.Y + 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X + 120 && clicked_box3.Location.Y == clicked_box34.Location.Y - 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 120 && clicked_box3.Location.Y == clicked_box34.Location.Y + 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                        if (clicked_box3.Location.X == clicked_box34.Location.X - 120 && clicked_box3.Location.Y == clicked_box34.Location.Y - 60)
                                        {
                                            if (clicked_box34.Tag == null) clicked_box34.BackColor = Color.Lime;
                                            else if (clicked_box34.Tag.ToString().Remove(1) == "b") clicked_box34.BackColor = Color.Red;
                                        }
                                    }
                                }

                                if (clicked_box3.Tag == "wr")
                                {
                                    inf_move_piece(clicked_box3, 0, 60, "b");
                                    inf_move_piece(clicked_box3, 0, -60, "b");
                                    inf_move_piece(clicked_box3, 60, 0, "b");
                                    inf_move_piece(clicked_box3, -60, 0, "b");
                                }
                                if (clicked_box3.Tag == "wb")
                                {
                                    inf_move_piece(clicked_box3, 60, 60, "b");
                                    inf_move_piece(clicked_box3, -60, -60, "b");
                                    inf_move_piece(clicked_box3, 60, -60, "b");
                                    inf_move_piece(clicked_box3, -60, 60, "b");
                                }
                                if (clicked_box3.Tag == "wq")
                                {
                                    inf_move_piece(clicked_box3, 0, 60, "b");
                                    inf_move_piece(clicked_box3, 0, -60, "b");
                                    inf_move_piece(clicked_box3, 60, 0, "b");
                                    inf_move_piece(clicked_box3, -60, 0, "b");
                                    inf_move_piece(clicked_box3, 60, 60, "b");
                                    inf_move_piece(clicked_box3, -60, -60, "b");
                                    inf_move_piece(clicked_box3, 60, -60, "b");
                                    inf_move_piece(clicked_box3, -60, 60, "b");
                                }

                                #endregion
                            }
                            if (clicked_box.Location.X == 300 && clicked_box.Location.Y == 60)
                            {
                                foreach (PictureBox clicked_box2 in Controls.OfType<PictureBox>())
                                {
                                    if (!rbrmoved)
                                    {
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 360 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition1 = true; clicked_box2.BackColor = Color.Pink; }
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 420 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition2 = true; clicked_box2.BackColor = Color.Pink; }
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 480 && clicked_box2.Tag == "br") { condition3 = true; clicked_box2.BackColor = Color.Pink; }
                                    }
                                    if (!lbrmoved)
                                    {
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 240 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition4 = true; clicked_box2.BackColor = Color.Pink; }
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 180 && clicked_box2.Tag == null && (clicked_box2.BackColor != Color.Red && clicked_box2.BackColor != Color.Lime)) { condition5 = true; clicked_box2.BackColor = Color.Pink; }
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 120 && clicked_box2.Tag == null) { condition6 = true; clicked_box2.BackColor = Color.Pink; }
                                        if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 60 && clicked_box2.Tag == "br") { condition7 = true; clicked_box2.BackColor = Color.Pink; }
                                    }
                                }
                            }
                            recolor();
                            if ((condition1 && condition2 && condition3) || (condition4 && condition5 && condition6 && condition7))
                            {
                                foreach (PictureBox clicked_box2 in Controls.OfType<PictureBox>())
                                {
                                    if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 420 && (condition1 && condition2 && condition3))
                                    {
                                        clicked_box2.BackColor = Color.Magenta;
                                    }
                                    if (clicked_box2.Location.Y == 60 && clicked_box2.Location.X == 180 && (condition4 && condition5 && condition6 && condition7))
                                    {
                                        clicked_box2.BackColor = Color.Magenta;
                                    }
                                }
                            }
                        }
                    }
                    //mark1:

                    if (extra) clicked_box.BackColor = Color.Yellow;

                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (clicked_box.Location.X == item.Location.X + temp_num && clicked_box.Location.Y == item.Location.Y + temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - temp_num && clicked_box.Location.Y == item.Location.Y + temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X + temp_num && clicked_box.Location.Y == item.Location.Y - temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - temp_num && clicked_box.Location.Y == item.Location.Y - temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - temp_num && clicked_box.Location.Y == item.Location.Y)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X + temp_num && clicked_box.Location.Y == item.Location.Y)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y - temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                    }
                }
                if (clicked_box.Tag.ToString() == temp + "n")
                {
                    if (extra)
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                    }
                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (clicked_box.Location.X == item.Location.X + temp_num && clicked_box.Location.Y == item.Location.Y + (2 * temp_num))
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - temp_num && clicked_box.Location.Y == item.Location.Y + (2 * temp_num))
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X + temp_num && clicked_box.Location.Y == item.Location.Y - (2 * temp_num))
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - temp_num && clicked_box.Location.Y == item.Location.Y - (2 * temp_num))
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X + (2 * temp_num) && clicked_box.Location.Y == item.Location.Y + temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - (2 * temp_num) && clicked_box.Location.Y == item.Location.Y + temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X + (2 * temp_num) && clicked_box.Location.Y == item.Location.Y - temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                        if (clicked_box.Location.X == item.Location.X - (2 * temp_num) && clicked_box.Location.Y == item.Location.Y - temp_num)
                        {
                            if (item.Tag == null) item.BackColor = Color.Lime;
                            else if (item.Tag.ToString().Remove(1) == temp2) item.BackColor = Color.Red;
                        }
                    }
                }

                if (clicked_box.Tag.ToString() == temp + "r")
                {
                    if (extra)
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                    }

                    inf_move_piece(clicked_box, 0, 60, temp2);
                    inf_move_piece(clicked_box, 0, -60, temp2);
                    inf_move_piece(clicked_box, 60, 0, temp2);
                    inf_move_piece(clicked_box, -60, 0, temp2);
                }
                if (clicked_box.Tag.ToString() == temp + "b")
                {
                    if (extra)
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                    }

                    inf_move_piece(clicked_box, 60, 60, temp2);
                    inf_move_piece(clicked_box, -60, -60, temp2);
                    inf_move_piece(clicked_box, 60, -60, temp2);
                    inf_move_piece(clicked_box, -60, 60, temp2);
                }
                if (clicked_box.Tag.ToString() == temp + "q")
                {
                    if (extra)
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                    }

                    inf_move_piece(clicked_box, 0, 60, temp2);
                    inf_move_piece(clicked_box, 0, -60, temp2);
                    inf_move_piece(clicked_box, 60, 0, temp2);
                    inf_move_piece(clicked_box, -60, 0, temp2);
                    inf_move_piece(clicked_box, 60, 60, temp2);
                    inf_move_piece(clicked_box, -60, -60, temp2);
                    inf_move_piece(clicked_box, 60, -60, temp2);
                    inf_move_piece(clicked_box, -60, 60, temp2);
                }
            }
        }
        public void recolor()
        {
            int counter = 0;
            int counter2 = 0;
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (counter % 2 == 0)
                {
                    item.BackColor = Color.Beige;

                }
                else if (counter % 2 == 1)
                {
                    item.BackColor = Color.RoyalBlue;

                }
                counter++;
                counter2++;
                if (counter2 % 8 == 0) counter++;
            }
        }
        public void update_pic()
        {
            foreach (PictureBox item in this.Controls.OfType<PictureBox>())
            {
                if (item.Tag == null) item.Image = null;
                else
                {
                    if (item.Tag.ToString() == "wr") item.Image = chess___.Properties.Resources.wr;
                    if (item.Tag.ToString() == "wn") item.Image = chess___.Properties.Resources.wn;
                    if (item.Tag.ToString() == "wb") item.Image = chess___.Properties.Resources.wb;
                    if (item.Tag.ToString() == "wk") item.Image = chess___.Properties.Resources.wk;
                    if (item.Tag.ToString() == "wq") item.Image = chess___.Properties.Resources.wq;
                    if (item.Tag.ToString() == "wp") item.Image = chess___.Properties.Resources.wp;
                    if (item.Tag.ToString() == "br") item.Image = chess___.Properties.Resources.br;
                    if (item.Tag.ToString() == "bn") item.Image = chess___.Properties.Resources.bn;
                    if (item.Tag.ToString() == "bb") item.Image = chess___.Properties.Resources.bb;
                    if (item.Tag.ToString() == "bk") item.Image = chess___.Properties.Resources.bk;
                    if (item.Tag.ToString() == "bq") item.Image = chess___.Properties.Resources.bq;
                    if (item.Tag.ToString() == "bp") item.Image = chess___.Properties.Resources.bp;
                }
            }
        }
        public void debug_tag_reci(PictureBox aa)
        {
            //if (aa.Tag != null)
            {
                MessageBox.Show(aa.Name);
                if (aa.Tag != null) MessageBox.Show(aa.Tag.ToString());
            }
        }
        public void inf_move_piece(PictureBox clicked_box, int l, int r, string xxx)
        {
            int kk = 1;
            bool aaa;
            do
            {
                aaa = false;
                foreach (PictureBox item in Controls.OfType<PictureBox>())
                {
                    if (clicked_box.Location.X == item.Location.X + l * kk && clicked_box.Location.Y == item.Location.Y + r * kk && item.Tag == null)
                    {
                        item.BackColor = Color.Lime;
                        kk++;
                        aaa = true;
                    }
                    else if (clicked_box.Location.X == item.Location.X + l * kk && clicked_box.Location.Y == item.Location.Y + r * kk && item.Tag.ToString().Remove(1) == xxx)
                    {
                        item.BackColor = Color.Red;
                        break;
                    }
                    else if (clicked_box.Location.X == item.Location.X + l * kk && clicked_box.Location.Y == item.Location.Y + r * kk && item.Tag != null)
                    {
                        break;
                    }
                }
            } while (aaa);
        }

        public bool change_turn(bool a)
        {
            if (a)
            {
                Text = "Black's turn";
                return false;
            }
            else
            {
                Text = "White's turn";
                return true;
            }
        }

        PictureBox floating_piece = new PictureBox();

        string[] curr_field = new string[64];

        string transform_to = "";
        string transform_this = "";

        bool movement = false;
        bool white_turn = true;

        bool whitechecked = false;
        bool blackchecked = false;

        bool wkmoved = false;
        bool bkmoved = false;
        bool rbrmoved = false;
        bool lbrmoved = false;
        bool rwrmoved = false;
        bool lwrmoved = false;

        public void main(PictureBox clicked_box)
        {
            if (!movement)
            {
                if (!white_turn)
                {
                    all_pieces(clicked_box, "b", true);

                    #region bm
                    /*
                    if (clicked_box.Tag == "bp")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y - 60 && item.Tag == null) item.BackColor = Color.Lime;
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y - 120 && item.Location.Y == 240 && item.Tag == null) item.BackColor = Color.Lime;
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y - 60 && item.Tag != null && item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y - 60 && item.Tag != null && item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                        }
                    }
                    if (clicked_box.Tag == "bk")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                        }
                    }
                    if (clicked_box.Tag == "bn")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y + 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y - 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y + 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y - 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 120 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 120 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 120 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 120 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "w") item.BackColor = Color.Red;
                            }
                        }
                    }

                    if (clicked_box.Tag == "br")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        inf_move_piece(clicked_box, 0, 60, "w");
                        inf_move_piece(clicked_box, 0, -60, "w");
                        inf_move_piece(clicked_box, 60, 0, "w");
                        inf_move_piece(clicked_box, -60, 0, "w");
                    }
                    if (clicked_box.Tag == "bb")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        inf_move_piece(clicked_box, 60, 60, "w");
                        inf_move_piece(clicked_box, -60, -60, "w");
                        inf_move_piece(clicked_box, 60, -60, "w");
                        inf_move_piece(clicked_box, -60, 60, "w");
                    }
                    if (clicked_box.Tag == "bq")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        inf_move_piece(clicked_box, 0, 60, "w");
                        inf_move_piece(clicked_box, 0, -60, "w");
                        inf_move_piece(clicked_box, 60, 0, "w");
                        inf_move_piece(clicked_box, -60, 0, "w");
                        inf_move_piece(clicked_box, 60, 60, "w");
                        inf_move_piece(clicked_box, -60, -60, "w");
                        inf_move_piece(clicked_box, 60, -60, "w");
                        inf_move_piece(clicked_box, -60, 60, "w");
                    }
                    */
                    #endregion

                    {
                        string name = null;

                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Tag != null && item == clicked_box)
                            {
                                name = item.Tag.ToString();
                                item.Tag = null;

                            }
                        }

                        List<string> green_spaces_to_skip = new List<string>();
                        List<string> names = new List<string>();
                        string[] current_field = new string[64];

                        //MessageBox.Show("Test");

                        int ii = 0;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Tag != null) current_field[ii] = item.Tag.ToString();
                            if (item.BackColor == Color.Lime || item.BackColor == Color.Red)
                            {
                                names.Add(item.Name);
                            }
                            ii++;
                        }

                        foreach (string item in names)
                        {
                            foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                            {
                                if (item == item2.Name)
                                {
                                    item2.Tag = name;
                                    recolor();
                                    foreach (PictureBox item5 in Controls.OfType<PictureBox>())
                                    {
                                        all_pieces(item5, "w", false);
                                    }
                                    foreach (PictureBox item5 in Controls.OfType<PictureBox>())
                                    {
                                        if (item5.Tag == "bk" && item5.BackColor == Color.Red)
                                        {
                                            green_spaces_to_skip.Add(item2.Name);
                                        }
                                    }
                                    ii = 0;
                                    foreach (PictureBox item3 in Controls.OfType<PictureBox>())
                                    {
                                        item3.Tag = current_field[ii];
                                        ii++;
                                    }
                                    recolor();
                                    update_pic();
                                }
                            }
                        }

                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Name == clicked_box.Name) item.Tag = name;
                        }

                        recolor();
                        update_pic();

                        all_pieces(clicked_box, "b", true);

                        foreach (string item in green_spaces_to_skip)
                        {
                            foreach (PictureBox item7 in Controls.OfType<PictureBox>())
                            {
                                if (item == item7.Name)
                                {
                                    if (item7.WaitOnLoad == true) item7.BackColor = Color.Beige;
                                    if (item7.WaitOnLoad == false) item7.BackColor = Color.RoyalBlue;
                                }
                            }
                        }
                    }

                }
                else
                {

                    all_pieces(clicked_box, "w", true);

                    #region wm
                    /*
                    if (clicked_box.Tag == "wp")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + 60 && item.Tag == null) item.BackColor = Color.Lime;
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + 120 && item.Location.Y == 300 && item.Tag == null) item.BackColor = Color.Lime;
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y + 60 && item.Tag != null && item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y + 60 && item.Tag != null && item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                        }
                    }
                    if (clicked_box.Tag == "wk")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                        }
                    }
                    if (clicked_box.Tag == "wn")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y + 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 60 && clicked_box.Location.Y == item.Location.Y - 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y + 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 60 && clicked_box.Location.Y == item.Location.Y - 120)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 120 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X + 120 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 120 && clicked_box.Location.Y == item.Location.Y + 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                            if (clicked_box.Location.X == item.Location.X - 120 && clicked_box.Location.Y == item.Location.Y - 60)
                            {
                                if (item.Tag == null) item.BackColor = Color.Lime;
                                else if (item.Tag.ToString().Remove(1) == "b") item.BackColor = Color.Red;
                            }
                        }
                    }

                    if (clicked_box.Tag == "wr")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        inf_move_piece(clicked_box, 0, 60, "b");
                        inf_move_piece(clicked_box, 0, -60, "b");
                        inf_move_piece(clicked_box, 60, 0, "b");
                        inf_move_piece(clicked_box, -60, 0, "b");
                    }
                    if (clicked_box.Tag == "wb")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        inf_move_piece(clicked_box, 60, 60, "b");
                        inf_move_piece(clicked_box, -60, -60, "b");
                        inf_move_piece(clicked_box, 60, -60, "b");
                        inf_move_piece(clicked_box, -60, 60, "b");
                    }
                    if (clicked_box.Tag == "wq")
                    {
                        movement = true;
                        floating_piece = clicked_box;
                        clicked_box.BackColor = Color.Yellow;

                        inf_move_piece(clicked_box, 0, 60, "b");
                        inf_move_piece(clicked_box, 0, -60, "b");
                        inf_move_piece(clicked_box, 60, 0, "b");
                        inf_move_piece(clicked_box, -60, 0, "b");
                        inf_move_piece(clicked_box, 60, 60, "b");
                        inf_move_piece(clicked_box, -60, -60, "b");
                        inf_move_piece(clicked_box, 60, -60, "b");
                        inf_move_piece(clicked_box, -60, 60, "b");
                    }
                    */
                    #endregion

                    {
                        string name = null;

                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Tag != null && item == clicked_box)
                            {
                                name = item.Tag.ToString();
                                item.Tag = null;

                            }
                        }

                        List<string> green_spaces_to_skip = new List<string>();
                        List<string> names = new List<string>();
                        string[] current_field = new string[64];

                        //MessageBox.Show("Test");

                        int ii = 0;
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Tag != null) current_field[ii] = item.Tag.ToString();
                            if (item.BackColor == Color.Lime || item.BackColor == Color.Red)
                            {
                                names.Add(item.Name);
                            }
                            ii++;
                        }

                        foreach (string item in names)
                        {
                            foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                            {
                                if (item == item2.Name)
                                {
                                    item2.Tag = name;
                                    recolor();
                                    foreach (PictureBox item5 in Controls.OfType<PictureBox>())
                                    {
                                        all_pieces(item5, "b", false);
                                    }
                                    foreach (PictureBox item5 in Controls.OfType<PictureBox>())
                                    {
                                        if (item5.Tag == "wk" && item5.BackColor == Color.Red)
                                        {
                                            green_spaces_to_skip.Add(item2.Name);
                                        }
                                    }
                                    ii = 0;
                                    foreach (PictureBox item3 in Controls.OfType<PictureBox>())
                                    {
                                        item3.Tag = current_field[ii];
                                        ii++;
                                    }
                                    recolor();
                                    update_pic();
                                }
                            }
                        }

                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Name == clicked_box.Name) item.Tag = name;
                        }

                        recolor();
                        update_pic();

                        all_pieces(clicked_box, "w", true);

                        foreach (string item in green_spaces_to_skip)
                        {
                            foreach (PictureBox item7 in Controls.OfType<PictureBox>())
                            {
                                if (item == item7.Name)
                                {
                                    if (item7.WaitOnLoad == true) item7.BackColor = Color.Beige;
                                    if (item7.WaitOnLoad == false) item7.BackColor = Color.RoyalBlue;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (clicked_box.BackColor == Color.Yellow)
                {
                    movement = false;
                    recolor();
                }
                if (clicked_box.BackColor == Color.Lime || clicked_box.BackColor == Color.Red)
                {
                    if (floating_piece.Tag == "wk") wkmoved = true;
                    if (floating_piece.Tag == "bk") bkmoved = true;
                    if (floating_piece.Tag == "br" && floating_piece.Location.X == 480 && floating_piece.Location.Y == 60) { rbrmoved = true; }
                    if (floating_piece.Tag == "br" && floating_piece.Location.X == 60 && floating_piece.Location.Y == 60) { lbrmoved = true; }
                    if (floating_piece.Tag == "wr" && floating_piece.Location.X == 480 && floating_piece.Location.Y == 480) { rwrmoved = true; }
                    if (floating_piece.Tag == "wr" && floating_piece.Location.X == 60 && floating_piece.Location.Y == 480) { lwrmoved = true; }

                    string temp_tag = floating_piece.Tag.ToString();

                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (item == floating_piece) item.Tag = null;
                    }
                    //zbog nekog razloga "floating_piece.Tag.ToString()" neradi poslije gornjeg koda

                    clicked_box.Tag = temp_tag;


                    //peon conversion
                    if ((clicked_box.Tag == "wp" && clicked_box.Location.Y == 60)
                      || (clicked_box.Tag == "bp" && clicked_box.Location.Y == 480))
                    {
                        transform_this = clicked_box.Name.ToString();
                        foreach (Panel item in Controls.OfType<Panel>())
                        {
                            item.Visible = true;
                            foreach (PictureBox item2 in Controls.OfType<PictureBox>())
                            {
                                item2.Enabled = false;
                            }
                        }
                    }


                    //check if king checked
                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        all_pieces(item, "w", false);
                    }
                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (item.BackColor == Color.Red && item.Tag == "bk")
                        {
                            blackchecked = true;
                        }
                    }
                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        all_pieces(item, "b", false);
                    }
                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (item.BackColor == Color.Red && item.Tag == "wk")
                        {
                            whitechecked = true;
                        }
                    }




                    movement = false;
                    white_turn = change_turn(white_turn);
                    update_pic();
                    recolor();

                    

                }
                if (clicked_box.BackColor == Color.Magenta)
                {
                    if (floating_piece.Tag == "wk") wkmoved = true;
                    if (floating_piece.Tag == "bk") bkmoved = true;

                    string temp_tag = floating_piece.Tag.ToString();
                    string temp_tag2 = floating_piece.Tag.ToString();

                    if (clicked_box.Location.Y == 480 && clicked_box.Location.X == 420)
                    {
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 480 && item.Location.X == 480)
                            {

                                temp_tag2 = item.Tag.ToString();
                                item.Tag = null;
                            }
                        }
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 480 && item.Location.X == 360)
                            {
                                item.Tag = temp_tag2;
                            }
                        }
                    }
                    if (clicked_box.Location.Y == 480 && clicked_box.Location.X == 180)
                    {
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 480 && item.Location.X == 60)
                            {

                                temp_tag2 = item.Tag.ToString();
                                item.Tag = null;
                            }
                        }
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 480 && item.Location.X == 240)
                            {
                                item.Tag = temp_tag2;
                            }
                        }
                    }

                    if (clicked_box.Location.Y == 60 && clicked_box.Location.X == 420)
                    {
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 60 && item.Location.X == 480)
                            {

                                temp_tag2 = item.Tag.ToString();
                                item.Tag = null;
                            }
                        }
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 60 && item.Location.X == 360)
                            {
                                item.Tag = temp_tag2;
                            }
                        }
                    }
                    if (clicked_box.Location.Y == 60 && clicked_box.Location.X == 180)
                    {
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 60 && item.Location.X == 60)
                            {

                                temp_tag2 = item.Tag.ToString();
                                item.Tag = null;
                            }
                        }
                        foreach (PictureBox item in Controls.OfType<PictureBox>())
                        {
                            if (item.Location.Y == 60 && item.Location.X == 240)
                            {
                                item.Tag = temp_tag2;
                            }
                        }
                    }

                    foreach (PictureBox item in Controls.OfType<PictureBox>())
                    {
                        if (item == floating_piece) item.Tag = null;
                    }
                    //zbog nekog razloga "floating_piece.Tag.ToString()" neradi poslije gornjeg koda

                    clicked_box.Tag = temp_tag;

                    movement = false;
                    white_turn = change_turn(white_turn);
                    update_pic();
                    recolor();
                }
            }
        }
    }
}
