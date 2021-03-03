public class MaskHeartController : BaseHeart, IGetableMaskHeart
{
    public void GotMaskHeart()
    {
        var nowTransform = transform;
        if (GameManager.Instance.currentState == GameManager.GameState.Playing_Heart0)
        {
            GameManager.Instance.dispatch(GameManager.GameState.Playing_Heart1);
            Instantiate(getItemEffect, nowTransform.position, nowTransform.rotation);
            Destroy(gameObject);
            return;
        }
        GameManager.Instance.dispatch(GameManager.GameState.Clear);
        Instantiate(getItemEffect, nowTransform.position, nowTransform.rotation);
        Destroy(gameObject);
    }
}