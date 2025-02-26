namespace View
{
    public class MyNormalView : MainView
    {
        private int spawnObjectsCount;
        private bool isWrongCollision;

        protected override void Init()
        {
            base.Init();

            triggerZone.OnTriggerNegative += FireTriggerLevelFailed;
        }
    }
}