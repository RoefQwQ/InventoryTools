# 待汉化内容 - Batch 3: 物品渲染器 (ItemRenderers)

> 本批次记录 InventoryTools/Logic/ItemRenderers/ 目录下需要汉化的内容
> 大部分 `Name`/`SingularName`/`PluralName` 已本地化，但 `HelpText` 和内部 UI 文本仍为英文

---

## 1. HelpText 属性（统一格式 "Can this item be ...?"）

以下文件中的 `HelpText` 属性需要汉化：

| 文件 | 英文 HelpText | 建议中文 |
|------|--------------|----------|
| ItemAchievementSourceRenderer.cs | `"Can this item be obtained from an achievement?"` | "此物品是否可以从成就中获得？" |
| ItemAirshipDropSourceRenderer.cs | `"Can this item be obtained from an airship voyage?"` | "此物品是否可以从飞空艇探索中获得？" |
| ItemAnimaShopSourceRenderer.cs | `"Can this item be purchased from the anima weapon shop?"` | "此物品是否可以从元灵武器商店购买？" |
| ItemAquariumUseRenderer.cs | `"Can this item be placed in an aquarium?"` | "此物品是否可以放入水族箱？" |
| ItemArmoireSourceRenderer.cs | `"Can this item be obtained from the armoire?"` | "此物品是否可以从衣柜中获得？" |
| ItemBattleLeveSourceRenderer.cs | `"Can this item be obtained from a battle leve?"` | "此物品是否可以从战斗理符中获得？" |
| ItemBuddySourceRenderer.cs | `"Can this item be obtained from a buddy?"` | "此物品是否可以从伙伴中获得？" |
| ItemCalamitySalvagerShopSourceRenderer.cs | `"Can this item be purchased from the calamity salvager?"` | "此物品是否可以从灾害 salvage 商人处购买？" |
| ItemCashShopSourceRenderer.cs | `"Can this item be purchased from the cash shop?"` | "此物品是否可以从商城购买？" |
| ItemCollectablesShopSourceRenderer.cs | `"Can this item be purchased from the collectables shop?"` | "此物品是否可以从收藏品商店购买？" |
| ItemCompanyCraftDraftSourceRenderer.cs | `"Can this item be obtained from a company craft draft?"` | "此物品是否可以从部队制作图纸中获得？" |
| ItemCompanyCraftRequirementSourceRenderer.cs | `"Can this item be obtained from a company craft requirement?"` | "此物品是否可以从部队制作需求中获得？" |
| ItemCompanyCraftResultSourceRenderer.cs | `"Can this item be obtained from a company craft result?"` | "此物品是否可以从部队制作结果中获得？" |
| ItemCompanyLeveSourceRenderer.cs | `"Can this item be obtained from a company leve?"` | "此物品是否可以从部队理符中获得？" |
| ItemCraftLeveSourceRenderer.cs | `"Can this item be obtained from a craft leve?"` | "此物品是否可以从制作理符中获得？" |
| ItemCraftLeveUseRenderer.cs | `"Can this item be used in a craft leve?"` | "此物品是否可以用于制作理符？" |
| ItemCraftRequirementSourceRenderer.cs | `"Can this item be obtained from a craft requirement?"` | "此物品是否可以从制作需求中获得？" |
| ItemCraftResultSourceRenderer.cs | `"Can this item be obtained from a craft result?"` | "此物品是否可以从制作结果中获得？" |
| ItemCustomDeliverySourceRenderer.cs | `"Can this item be obtained from a custom delivery?"` | "此物品是否可以从老主顾交付中获得？" |
| ItemDungeonBossChestSourceRenderer.cs | `"Can this item be obtained from a dungeon boss chest?"` | "此物品是否可以从副本首领宝箱中获得？" |
| ItemDungeonBossDropSourceRenderer.cs | `"Can this item be obtained from a dungeon boss drop?"` | "此物品是否可以从副本首领掉落中获得？" |
| ItemDungeonChestSourceRenderer.cs | `"Can this item be obtained from a dungeon chest?"` | "此物品是否可以从副本宝箱中获得？" |
| ItemDungeonDropSourceRenderer.cs | `"Can this item be obtained from a dungeon drop?"` | "此物品是否可以从副本掉落中获得？" |
| ItemExteriorFurnitureSourceRenderer.cs | `"Can this item be obtained from exterior furniture?"` | "此物品是否可以从室外家具中获得？" |
| ItemFateShopSourceRenderer.cs | `"Can this item be purchased from a FATE shop?"` | "此物品是否可以从 FATE 商店购买？" |
| ItemFateSourceRenderer.cs | `"Can this item be obtained from a FATE?"` | "此物品是否可以从 FATE 中获得？" |
| ItemFccShopSourceRenderer.cs | `"Can this item be purchased from the free company chest shop?"` | "此物品是否可以从部队箱商店购买？" |
| ItemFieldOpCofferSourceRenderer.cs | `"Can this item be obtained from a field operation coffer?"` | "此物品是否可以从野外作战宝箱中获得？" |
| ItemFishingSourceRenderer.cs | `"Can this item be obtained from fishing?"` | "此物品是否可以从钓鱼中获得？" |
| ItemFurnitureSourceRenderer.cs | `"Can this item be obtained from furniture?"` | "此物品是否可以从家具中获得？" |
| ItemGCExpertDeliverySourceRenderer.cs | `"Can this item be obtained from a GC expert delivery?"` | "此物品是否可以从军队专家交付中获得？" |
| ItemGCShopSourceRenderer.cs | `"Can this item be purchased from a GC shop?"` | "此物品是否可以从军队商店购买？" |
| ItemGCSupplyDutySourceRenderer.cs | `"Can this item be obtained from a GC supply duty?"` | "此物品是否可以从军队筹备任务中获得？" |
| ItemGardeningCrossbreedSourceRenderer.cs | `"Can this item be obtained from gardening crossbreeding?"` | "此物品是否可以从园艺杂交中获得？" |
| ItemGatheringLeveSourceRenderer.cs | `"Can this item be obtained from a gathering leve?"` | "此物品是否可以从采集理符中获得？" |
| ItemGatheringSourceRenderer.cs | `"Can this item be obtained from gathering?"` | "此物品是否可以从采集中获得？" |
| ItemGearsetUseRenderer.cs | `"Can this item be used in a gearset?"` | "此物品是否可以用于套装？" |
| ItemGilShopSourceRenderer.cs | `"Can this item be purchased from a gil shop?"` | "此物品是否可以从金币商店购买？" |
| ItemGlamourReadySetSourceRenderer.cs | `"Can this item be obtained from a glamour ready set?"` | "此物品是否可以从幻化套装中获得？" |
| ItemGlamourReadySourceRenderer.cs | `"Can this item be obtained from a glamour ready source?"` | "此物品是否可以从幻化解锁来源中获得？" |
| ItemHouseSourceRenderer.cs | `"Can this item be obtained from a house?"` | "此物品是否可以从房屋中获得？" |
| ItemMonsterDropSourceRenderer.cs | `"Can this item be obtained from a monster drop?"` | "此物品是否可以从怪物掉落中获得？" |
| ItemPvpSeriesSourceRenderer.cs | `"Can this item be obtained from a PvP series?"` | "此物品是否可以从 PvP 系列赛获得？" |
| ItemQuestSourceRenderer.cs | `"Can this item be obtained from a quest?"` | "此物品是否可以从任务中获得？" |
| ItemQuickVentureSourceRenderer.cs | `"Can this item be obtained from a quick venture?"` | "此物品是否可以从快速探险中获得？" |
| ItemRelicWeaponSourceRenderer.cs | `"Can this item be obtained from a relic weapon?"` | "此物品是否可以从古武武器中获得？" |
| ItemSkybuilderHandInSourceRenderer.cs | `"Can this item be obtained from a skybuilder hand-in?"` | "此物品是否可以从天穹街交纳中获得？" |
| ItemSkybuilderInspectionSourceRenderer.cs | `"Can this item be obtained from a skybuilder inspection?"` | "此物品是否可以从天穹街检查中获得？" |
| ItemSpearFishingSourceRenderer.cs | `"Can this item be obtained from spearfishing?"` | "此物品是否可以从刺鱼中获得？" |
| ItemSpecialShopSourceRenderer.cs | `"Can this item be purchased from a special shop?"` | "此物品是否可以从特殊商店购买？" |
| ItemStainUseRenderer.cs | `"Can this item be used as a stain?"` | "此物品是否可以作为染色剂使用？" |
| ItemSubmarineDropSourceRenderer.cs | `"Can this item be obtained from a submarine drop?"` | "此物品是否可以从潜水艇探索中获得？" |
| ItemSupplementSourceRenderer.cs | `"Can this item be obtained from a supplement?"` | "此物品是否可以从补充包中获得？" |
| ItemToolWeaponSourceRenderer.cs | `"Can this item be obtained from a tool weapon?"` | "此物品是否可以从工具武器中获得？" |
| ItemTripleTriadSourceRenderer.cs | `"Can this item be obtained from Triple Triad?"` | "此物品是否可以从九宫幻卡中获得？" |
| ItemVentureSourceRenderer.cs | `"Can this item be obtained from a venture?"` | "此物品是否可以从探险中获得？" |

---

## 2. 内部 UI 文本（非 HelpText）

| 文件 | 行号 | 英文 | 建议中文 |
|------|------|------|----------|
| ItemSupplementSourceRenderer.cs | ~391 | `"(Drops 1)"` | "（掉落 1 个）" |
| ItemSupplementSourceRenderer.cs | ~395 | `"(Drops "` + min + `" - "` + max + `")"` | "（掉落 " + min + " - " + max + " 个）" |
| ItemSupplementSourceRenderer.cs | ~418 | `"(Drops 1)"` | "（掉落 1 个）" |
| ItemSupplementSourceRenderer.cs | ~422 | `"(Drops "` + min + `" - "` + max + `")"` | "（掉落 " + min + " - " + max + " 个）" |
| ItemStainUseRenderer.cs | ~30 | `"Colour: "` | "颜色：" |
| ItemSpecialShopSourceRenderer.cs | ~71 | `"Rewards:"` | "奖励：" |
| ItemSpecialShopSourceRenderer.cs | ~90 | `"Costs:"` | "成本：" |
| ItemSpearFishingSourceRenderer.cs | ~39 | `"Level:"` | "等级：" |
| ItemSpearFishingSourceRenderer.cs | ~49 | `"Level:"` | "等级：" |
| ItemSkybuilderHandInSourceRenderer.cs | ~35 | `"Level: "` | "等级：" |
| ItemSkybuilderHandInSourceRenderer.cs | ~36 | `"Max Level: "` | "最高等级：" |
| ItemSkybuilderHandInSourceRenderer.cs | ~38 | `"Rewards:"` | "奖励：" |
| ItemSkybuilderHandInSourceRenderer.cs | ~41 | `"Exp: "` | "经验：" |
| ItemSkybuilderHandInSourceRenderer.cs | ~42 | `"Script: "` | "工票：" |
| ItemSkybuilderHandInSourceRenderer.cs | ~44 | `"Points: "` | "点数：" |
| ItemPvpSeriesSourceRenderer.cs | ~27 | `"Reward in PVP Series "` | "PvP 系列赛奖励 " |
| ItemPvpSeriesSourceRenderer.cs | ~28 | `"Unlocks at level "` | "解锁等级 " |
| ItemQuestSourceRenderer.cs | ~53 | `"Name: "` | "名称：" |
| ItemQuestSourceRenderer.cs | ~54 | `"Expansion: "` | "资料片：" |
| ItemQuestSourceRenderer.cs | ~57 | `"Allied Society: "` | "友好部族：" |
| ItemQuestSourceRenderer.cs | ~62 | `"Only available from "` | "仅可从 " |
| ItemQuestSourceRenderer.cs | ~121 | `"Name: "` | "名称：" |
| ItemQuestSourceRenderer.cs | ~122 | `"Expansion: "` | "资料片：" |

---

## 3. ItemInfoRenderCategory.cs

该文件为枚举类型，枚举成员名称为代码标识符，通常不需要本地化。
但如果在 UI 中显示，可能需要考虑汉化显示文本。

---

## 总结

本批次共涉及 **60+ 个 ItemRenderer 文件**，主要是：
1. **HelpText 属性**：统一格式 "Can this item be ...?" → "此物品是否可以...？"
2. **内部 UI 文本**：标签、标题、提示等

建议采用批量替换的方式处理 HelpText，因为它们遵循统一的模式。
