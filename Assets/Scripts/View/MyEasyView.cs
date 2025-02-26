namespace View
{
    public class MyEasyView : MainView
    {
        private int spawnObjectsCount;

        protected override void Init()
        {
            base.Init();

            triggerZone.OnTriggerNegative += FireTriggerLevelFailed;
        }

        public override void FireTriggerLevelFailed()
        {
            base.FireTriggerLevelFailed();
            triggerZone.OnTriggerNegative -= FireTriggerLevelFailed;
        }
    }
}