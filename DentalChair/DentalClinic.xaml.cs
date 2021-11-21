using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DentalChair
{
    public partial class DentalClinic : Window
    {
        #region Variables

        RotateTransform chairRotate = new RotateTransform();
        RotateTransform chairBackRotate = new RotateTransform();
        RotateTransform lightHandleRotate = new RotateTransform();
        TranslateTransform tf = new TranslateTransform();
        double previousX = 0;
        double previousY = 0;
        float angle = 0;
        float chairBackangle = 0;
        int lightHandleangle = 0;
        double previousheight = 0;
        double height = 0;

        #endregion

        #region Constructor

        public DentalClinic()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void BulbSwitch_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //If light rays visible
            if (LightRay.IsVisible)
            {
                LightRay.Visibility = Visibility.Hidden;
            }
            else
            {
                LightRay.Visibility = Visibility.Visible;
            }
        }

        private void Path_58_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //If water fall animation visible
            if (Water.IsVisible)
            {
                Water.Visibility = Visibility.Hidden;
            }
            else
            {
                Water.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LightRay.Visibility = Visibility.Hidden;
            Water.Visibility = Visibility.Hidden;
            Storyboard SB1 = Resources["FanRotate"] as Storyboard;
            SB1.Stop();
        }

        private void BaseButtonLeft_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Check if the angle less than -2 for backwards bend.
            if (angle > -2)
            {
                //Decrease the angle by .5 for each click
                angle -= 0.5f;
                //The angle is assigned to rotation trnasform object.
                chairRotate.Angle = angle;
                //Assign the x and y axis to 0 to rotate the chair at that point.
                chairRotate.CenterX = 0;
                chairRotate.CenterY = 0;
                //Finally assign the transform object to tbe rendered to the seatbase that is midstand.
                MidStand.RenderTransform = chairRotate;
            }
        }

        private void BaseButtonRight_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Check if the angle less than or equal to 12.
            if (angle <= 12)
            {
                //Increment the angle by .5 if user clicks the down button
                angle += 0.5f;
                //The angle is assigned to rotation trnasform object.
                chairRotate.Angle = angle;
                //Assign the x and y axis to 0 to rotate the chair at that point.
                chairRotate.CenterX = 0;
                chairRotate.CenterY = 0;
                //Finally assign the transform object to tbe rendered to the seatbase that is midstand.
                MidStand.RenderTransform = chairRotate;
            }
        }

        private void ChairSwitch_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Check if the angle is not less than -10 that is backward bend
            if (chairBackangle >= -10)
            {
                //Decrement the angle by .5
                chairBackangle -= 0.5f;
                //The angle is assigned to rotation trnasform object.
                chairBackRotate.Angle = chairBackangle;
                //Assign the x and y axis to 0 to rotate the chair at that point.
                chairBackRotate.CenterX = 0;
                chairBackRotate.CenterY = 0;
                //Finally assign the transform object to tbe rendered to the seatbase that is midstand.
                BackSeat.RenderTransform = chairBackRotate;
            }
        }

        private void LightPipe_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Capture the previous mouse point to decide whether to move up or down
            previousheight = height;
            //Capture the mouse movement at that point
            MouseDevice d = e.MouseDevice;
            Point p = d.GetPosition(this);
            height = p.Y;

            //Check whether the left button of the mouse is pressed or not.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Check if the angle less than 25 to limit the rotation
                if (lightHandleangle < 25)
                {
                    //If previous mouse point is greater than current
                    if (previousheight >= height)
                    {
                        //Increment the angle by 1.
                        lightHandleangle += 1;
                        //Assign the angle to the rotate transform object
                        lightHandleRotate.Angle = lightHandleangle;
                        //Assign the x and y axis to 0
                        lightHandleRotate.CenterX = 0;
                        lightHandleRotate.CenterY = 0;
                        //Assign the rotate trans object to handle to transform to that angle specified.
                        LightHandle.RenderTransform = lightHandleRotate;
                    }
                    //Perform the check to see if previous is less than current mouse position to move the light handle down and above the minmum height.
                    if (previousheight <= height && previousheight != 0 && lightHandleangle > -15)
                    {
                        //Decrement the angle by 1
                        lightHandleangle -= 1;
                        lightHandleRotate.Angle = lightHandleangle;
                        lightHandleRotate.CenterX = 0;
                        lightHandleRotate.CenterY = 0;
                        LightHandle.RenderTransform = lightHandleRotate;
                        return;
                    }
                }
                //Check if the angle is greater then the minimum height that is -15(downwards)
                else
                {
                    if (lightHandleangle > -15)
                    {
                        if (previousheight <= height && previousheight != 0)
                        {
                            lightHandleangle -= 1;
                            lightHandleRotate.Angle = lightHandleangle;
                            lightHandleRotate.CenterX = 0;
                            lightHandleRotate.CenterY = 0;
                            LightHandle.RenderTransform = lightHandleRotate;
                            return;
                        }
                    }
                }

            }
        }

        private void ChairSwitchRight_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (chairBackangle < 6)
            {
                chairBackangle += 0.5f;
                chairBackRotate.Angle = chairBackangle;
                chairBackRotate.CenterX = 0;
                chairBackRotate.CenterY = 0;
                BackSeat.RenderTransform = chairBackRotate;
            }
        }

        private void FireInstrument_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Capture the mouse points
                MouseDevice m = e.MouseDevice;
                Point p = m.GetPosition(this);
                if (previousX == 0)
                {
                    previousX = p.X;
                    previousY = p.Y;
                }
                //Assign the mouse points to instrument
                tf.X = p.X - previousX;
                tf.Y = p.Y - previousY;
                FirePipe.RenderTransform = tf;

            }
        }

        private void FButton_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Storyboard SB = Resources["FanRotate"] as Storyboard;
            SB.Begin();

        }

        private void Button_4_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Storyboard SB = Resources["FanRotate"] as Storyboard;
            SB.Pause();
        }

        #endregion
    }
}
