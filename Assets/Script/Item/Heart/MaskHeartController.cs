
public class MaskHeartController : BaseHeart, IGetableMaskHeart
{
    //MaskManがMaskHeartに触れたときに呼び出される
    public void GotMaskHeart()
    {
        //Heartが1つも取得されてなければStateを1つ取得した状態にしてEffectを出し、破壊する
        var nowTransform = transform;
        if (GameManager.Instance.currentState == GameManager.GameState.Playing_Heart0)
        {
            GameManager.Instance.dispatch(GameManager.GameState.Playing_Heart1);
            Instantiate(getItemEffect, nowTransform.position, nowTransform.rotation);
            Destroy(gameObject);
            return;
        }
        //Heartが既に1つ取得されていればStateをクリアに切り替えて状態にしてEffectを出し、破壊する
        GameManager.Instance.dispatch(GameManager.GameState.Clear);
        Instantiate(getItemEffect, nowTransform.position, nowTransform.rotation);
        Destroy(gameObject);
    }
}