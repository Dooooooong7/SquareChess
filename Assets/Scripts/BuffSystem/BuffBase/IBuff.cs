public interface IBuff
{
    // Buff 持续时间 (秒数或回合数)
    float Duration { get; }

    // Buff 是否有效 (是否还在起作用)
    bool IsEffective { get; set; }

    // Buff 当前层数 (部分 Buff 可以叠加)
    int Layer { get; set; }

    // 初始化 Buff（激活时调用）
    void OnBuffAwake();

    // 每回合结束时执行的逻辑 (如扣血、更新状态等)
    void OnBuffEndTurn();

    // 用于执行 Buff 层数相关的修改逻辑
    void RealModifyLayer();

    // 每回合定时执行的效果 (如定期扣血、增加攻击力等)
    void OnBuffTickEffect();
}