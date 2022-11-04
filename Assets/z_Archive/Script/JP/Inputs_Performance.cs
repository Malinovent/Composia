using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Composia.Arrangement
{
    public class Inputs_Performance : InputSystem
    {

        //PerformanceController performanceController = PerformanceController.Instance;
        public override void InitializeControls()
        {
            controls.Performance.Quit.performed += _ => Quit();
            controls.Performance.Retry.performed += _ => Retry();
            controls.Performance.PressRepeat.performed += _ => PressRepeat();
            controls.Performance.DownUp.performed += _ => JoystickVertical();
            controls.Performance.SetBPM.performed += ctx => SetBPM(ctx.ReadValue<float>());
            controls.Performance.Play.performed += _ => Play();
        }

        public void AddAction()
        { 
        
        }

        private void Play()
        {
            PerformanceController.Instance.Play();
        }
        private void Quit()
        {

        }

        private void Retry()
        {
            //Add condition as to when this is valid
            // PerformanceController.Instance.Restart();
        }

        private void PressRepeat()
        {

        }

        private void JoystickVertical()
        {

        }

        private void SetBPM(float value)
        {
            PerformanceController.Instance.SetBPM(value);
        }
    }
}