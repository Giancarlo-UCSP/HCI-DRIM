using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System;
using WiimoteApi;

public class WiimoteDemo : MonoBehaviour {

    //PlayerMove Begin
    public Transform tCamera;
    public float speed = 4.0f;
    private CharacterController cc;
    //PlayerMove End

    //Flashlight Begin
    public float PSpeed = 0;
    public float YSpeed = 0;
    public float RSpeed = 0;
    //Flashlight End

    public WiimoteModel model;
    public RectTransform[] ir_dots;
    public RectTransform[] ir_bb;
    public RectTransform ir_pointer;

    private Quaternion initial_rotation;

    private Wiimote wiimote;

    private Vector2 scrollPosition;

    private Vector3 wmpOffset = Vector3.zero;

    public WiimoteModel Flashlight;
    public Light Spotlight;
    public bool IsStarted;
    public MotionPlusData dataMP;
    public bool IsFixed;


    void Start() {
        initial_rotation = model.rot.localRotation;
        Spotlight.enabled = false;
        IsStarted = false;
        IsFixed = false;
        cc = GetComponent<CharacterController>();//PM

    }

	void Update () {
        if (!WiimoteManager.HasWiimote()) { return; }

        wiimote = WiimoteManager.Wiimotes[0];

        int ret = wiimote.ReadWiimoteData();
        while (ret > 0)
        {
            ret = wiimote.ReadWiimoteData();

            if (ret > 0 && wiimote.current_ext == ExtensionController.MOTIONPLUS)
            {
                PSpeed = wiimote.MotionPlus.PitchSpeed - 1.7f;
                if ( ((PSpeed < 400.0f) && (PSpeed > 4.0f)) || ((PSpeed < -4.0f) && (PSpeed > -400.0f)) )
                {
                    model.rot.Rotate(Vector3.forward, PSpeed * Time.deltaTime);
                }
                YSpeed = wiimote.MotionPlus.YawSpeed + 17.5f;
                if (((YSpeed < 400.0f) && (YSpeed > 4.0f)) || ((YSpeed < -4.0f) && (YSpeed > -400.0f)))
                {
                    model.rot.Rotate(-Vector3.up, YSpeed * Time.deltaTime);
                }
            }
        }

        if (wiimote.Button.d_up)
        {
            Vector3 forward = tCamera.TransformDirection(Vector3.forward);
            cc.SimpleMove(forward * speed);
        }
        else if (wiimote.Button.d_down)
        {
            Vector3 back = tCamera.TransformDirection(Vector3.back);
            cc.SimpleMove(back * speed);
        }
        else if (wiimote.Button.d_down)
        {
            Vector3 left = tCamera.TransformDirection(Vector3.left);
            cc.SimpleMove(left * speed);
        }
        else if (wiimote.Button.d_down)
        {
            Vector3 right = tCamera.TransformDirection(Vector3.right);
            cc.SimpleMove(right * speed);
        }

        /*model.a.enabled = wiimote.Button.a;
        model.b.enabled = wiimote.Button.b;
        model.one.enabled = wiimote.Button.one;
        model.two.enabled = wiimote.Button.two;
        model.d_up.enabled = wiimote.Button.d_up;
        model.d_down.enabled = wiimote.Button.d_down;
        model.d_left.enabled = wiimote.Button.d_left;
        model.d_right.enabled = wiimote.Button.d_right;
        model.plus.enabled = wiimote.Button.plus;
        model.minus.enabled = wiimote.Button.minus;
        model.home.enabled = wiimote.Button.home;*/

        Spotlight.enabled = wiimote.Button.a;

        Debug.Log(model.rot.localRotation);

        if (wiimote.Button.b && (IsFixed == false))
        {
            FixMovement();
            IsFixed = true;
        }

        if (wiimote.current_ext != ExtensionController.MOTIONPLUS)
            model.rot.localRotation = initial_rotation;

        if (ir_dots.Length < 4) return;

        float[,] ir = wiimote.Ir.GetProbableSensorBarIR();
        for (int i = 0; i < 2; i++)
        {
            float x = (float)ir[i, 0] / 1023f;
            float y = (float)ir[i, 1] / 767f;
            if (x == -1 || y == -1) {
                ir_dots[i].anchorMin = new Vector2(0, 0);
                ir_dots[i].anchorMax = new Vector2(0, 0);
            }

            ir_dots[i].anchorMin = new Vector2(x, y);
            ir_dots[i].anchorMax = new Vector2(x, y);

            if (ir[i, 2] != -1)
            {
                int index = (int)ir[i,2];
                float xmin = (float)wiimote.Ir.ir[index,3] / 127f;
                float ymin = (float)wiimote.Ir.ir[index,4] / 127f;
                float xmax = (float)wiimote.Ir.ir[index,5] / 127f;
                float ymax = (float)wiimote.Ir.ir[index,6] / 127f;
                ir_bb[i].anchorMin = new Vector2(xmin, ymin);
                ir_bb[i].anchorMax = new Vector2(xmax, ymax);
            }
        }

        float[] pointer = wiimote.Ir.GetPointingPosition();
        ir_pointer.anchorMin = new Vector2(pointer[0], pointer[1]);
        ir_pointer.anchorMax = new Vector2(pointer[0], pointer[1]);


	}

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, 320, Screen.height), "");

        GUILayout.BeginVertical(GUILayout.Width(300));
        GUILayout.Label("Wiimote Found: " + WiimoteManager.HasWiimote());
        if (GUILayout.Button("Find Wiimote"))
            WiimoteManager.FindWiimotes();

        if (GUILayout.Button("Cleanup"))
        {
            WiimoteManager.Cleanup(wiimote);
            wiimote = null;
        }

        if (wiimote == null)
            return;

        GUILayout.Label("Extension: " + wiimote.current_ext.ToString());

        GUILayout.Label("LED Test:");
        GUILayout.BeginHorizontal();
        for (int x = 0; x < 4;x++ )
            if (GUILayout.Button(""+x, GUILayout.Width(300/4)))
                wiimote.SendPlayerLED(x == 0, x == 1, x == 2, x == 3);
        GUILayout.EndHorizontal();

        GUILayout.Label("Set Report:");
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("But/Acc", GUILayout.Width(300/4)))
            wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL);
        if(GUILayout.Button("But/Ext8", GUILayout.Width(300/4)))
            wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_EXT8);
        if(GUILayout.Button("B/A/Ext16", GUILayout.Width(300/4)))
            wiimote.SendDataReportMode(InputDataType.REPORT_BUTTONS_ACCEL_EXT16);
        if(GUILayout.Button("Ext21", GUILayout.Width(300/4)))
            wiimote.SendDataReportMode(InputDataType.REPORT_EXT21);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Request Status Report"))
            wiimote.SendStatusInfoRequest();

        GUILayout.Label("IR Setup Sequence:");
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Basic", GUILayout.Width(100)))
            wiimote.SetupIRCamera(IRDataType.BASIC);
        if(GUILayout.Button("Extended", GUILayout.Width(100)))
            wiimote.SetupIRCamera(IRDataType.EXTENDED);
        if(GUILayout.Button("Full", GUILayout.Width(100)))
            wiimote.SetupIRCamera(IRDataType.FULL);
        GUILayout.EndHorizontal();

        GUILayout.Label("WMP Attached: " + wiimote.wmp_attached);
        if (GUILayout.Button("Request Identify WMP"))
            wiimote.RequestIdentifyWiiMotionPlus();
        if ((wiimote.wmp_attached || wiimote.Type == WiimoteType.PROCONTROLLER) && GUILayout.Button("Activate WMP"))
            wiimote.ActivateWiiMotionPlus();
        if ((wiimote.current_ext == ExtensionController.MOTIONPLUS ||
            wiimote.current_ext == ExtensionController.MOTIONPLUS_CLASSIC ||
            wiimote.current_ext == ExtensionController.MOTIONPLUS_NUNCHUCK) && GUILayout.Button("Deactivate WMP"))
            wiimote.DeactivateWiiMotionPlus();

        GUILayout.Label("Calibrate Accelerometer");
        GUILayout.BeginHorizontal();
        for (int x = 0; x < 3; x++)
        {
            AccelCalibrationStep step = (AccelCalibrationStep)x;
            if (GUILayout.Button(step.ToString(), GUILayout.Width(100)))
                wiimote.Accel.CalibrateAccel(step);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Print Calibration Data"))
        {
            StringBuilder str = new StringBuilder();
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    str.Append(wiimote.Accel.accel_calib[y, x]).Append(" ");
                }
                str.Append("\n");
            }
            Debug.Log(str.ToString());
        }

        if (wiimote != null && wiimote.current_ext != ExtensionController.NONE)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUIStyle bold = new GUIStyle(GUI.skin.button);
            bold.fontStyle = FontStyle.Bold;
            if (wiimote.current_ext == ExtensionController.NUNCHUCK) {
                GUILayout.Label("Nunchuck:", bold);
                NunchuckData data = wiimote.Nunchuck;
                GUILayout.Label("Stick: " + data.stick[0] + ", " + data.stick[1]);
                GUILayout.Label("C: " + data.c);
                GUILayout.Label("Z: " + data.z);
            } 
            else if (wiimote.current_ext == ExtensionController.MOTIONPLUS)
            {
                GUILayout.Label("Wii Motion Plus:", bold);
                //MotionPlusData data = wiimote.MotionPlus;
                dataMP = wiimote.MotionPlus;
                GUILayout.Label("Pitch Speed: " + dataMP.PitchSpeed);
                GUILayout.Label("Yaw Speed: " + dataMP.YawSpeed);
                GUILayout.Label("Roll Speed: " + dataMP.RollSpeed);
                GUILayout.Label("Pitch Slow: " + dataMP.PitchSlow);
                GUILayout.Label("Yaw Slow: " + dataMP.YawSlow);
                GUILayout.Label("Roll Slow: " + dataMP.RollSlow);
                /*
                if (GUILayout.Button("Zero Out WMP"))
                //if (IsStarted == false)
                {                
                    dataMP.SetZeroValues();
                    model.rot.rotation = Quaternion.FromToRotation(model.rot.rotation*GetAccelVector(), Vector3.up) * model.rot.rotation;
                    model.rot.rotation = Quaternion.FromToRotation(model.rot.forward, Vector3.forward) * model.rot.rotation;
                    Flashlight.rot.rotation = Quaternion.FromToRotation(Flashlight.rot.rotation*GetAccelVector(), Vector3.up) * Flashlight.rot.rotation;
                    Flashlight.rot.rotation = Quaternion.FromToRotation(Flashlight.rot.forward, Vector3.forward) * Flashlight.rot.rotation;
                    IsStarted = true;
                }
                */
                if(GUILayout.Button("Reset Offset"))
                    wmpOffset = Vector3.zero;
                GUILayout.Label("Offset: " + wmpOffset.ToString());
            }
            
			
            GUILayout.EndScrollView();
        } else {
            scrollPosition = Vector2.zero;
        }
        GUILayout.EndVertical();
    }

    void OnDrawGizmos()
    {
        if (wiimote == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(model.rot.position, model.rot.position + model.rot.rotation*GetAccelVector()*2);
    }

    void FixMovement()
    {
        dataMP.SetZeroValues();
        model.rot.rotation = Quaternion.FromToRotation(model.rot.rotation*GetAccelVector(), Vector3.up) * model.rot.rotation;
        model.rot.rotation = Quaternion.FromToRotation(model.rot.forward, Vector3.forward) * model.rot.rotation;
        Flashlight.rot.rotation = Quaternion.FromToRotation(Flashlight.rot.rotation*GetAccelVector(), Vector3.up) * Flashlight.rot.rotation;
        Flashlight.rot.rotation = Quaternion.FromToRotation(Flashlight.rot.forward, Vector3.forward) * Flashlight.rot.rotation;
    }

    private Vector3 GetAccelVector()
    {
        float accel_x;
        float accel_y;
        float accel_z;

        float[] accel = wiimote.Accel.GetCalibratedAccelData();
        accel_x = accel[0];
        accel_y = -accel[2];
        accel_z = -accel[1];

        return new Vector3(accel_x, accel_y, accel_z).normalized;
    }

    [System.Serializable]
    public class WiimoteModel
    {
        public Transform rot;
        public Renderer a;
        public Renderer b;
        public Renderer one;
        public Renderer two;
        public Renderer d_up;
        public Renderer d_down;
        public Renderer d_left;
        public Renderer d_right;
        public Renderer plus;
        public Renderer minus;
        public Renderer home;
    }

	void OnApplicationQuit() {
		if (wiimote != null) {
			WiimoteManager.Cleanup(wiimote);
	        wiimote = null;
		}
	}
}
