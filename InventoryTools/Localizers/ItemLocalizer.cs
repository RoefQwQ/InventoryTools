using System.Collections.Generic;
using CriticalCommonLib.Enums;
using CriticalCommonLib.Models;
using InventoryTools.Services;
using Lumina.Excel;
using Lumina.Excel.Sheets;

namespace InventoryTools.Localizers;

public class ItemLocalizer
{
    private readonly ExcelSheet<Addon> _addonSheet;
    private readonly ILocalizationService _localizationService;
    private Dictionary<uint, string> _cabinetNames;

    public ItemLocalizer(ExcelSheet<Addon> addonSheet, ILocalizationService localizationService)
    {
        _addonSheet = addonSheet;
        _localizationService = localizationService;
        _cabinetNames = new();
    }

    public string CabinetName(InventoryItem inventoryItem)
    {
        if (inventoryItem.SortedContainer != InventoryType.Armoire)
        {
            return "";
        }

        var cabinetCategory = inventoryItem.Item.CabinetCategory;
        if (cabinetCategory == null)
        {
            return _localizationService.GetString("Item_UnknownCabinet");
        }

        if (_cabinetNames.TryGetValue(cabinetCategory.Base.Category.RowId, out string? cabinetName))
        {
            return cabinetName;
        }

        cabinetName = _addonSheet.GetRowOrDefault(cabinetCategory.Base.Category.RowId)?.Text.ExtractText() ??
                      _localizationService.GetString("Item_AddonTextNotFound");

        _cabinetNames[cabinetCategory.Base.Category.RowId] = cabinetName;

        return cabinetName;
    }

    public string ItemDescription(InventoryItem inventoryItem)
    {
        if (inventoryItem.IsEmpty)
        {
            return _localizationService.GetString("Item_Empty");
        }

        var _item = inventoryItem.Item.NameString.ToString();
        if (inventoryItem.IsHQ)
        {
            _item += _localizationService.GetString("Item_HQ");
        }
        else if (inventoryItem.IsCollectible)
        {
            _item += _localizationService.GetString("Item_Collectible");
        }
        else
        {
            _item += _localizationService.GetString("Item_NQ");
        }

        if (inventoryItem.SortedCategory == InventoryCategory.Currency)
        {
            _item += " - " + SortedContainerName(inventoryItem);
        }
        else
        {
            _item += " - " + SortedContainerName(inventoryItem) + " - " + (inventoryItem.SortedSlotIndex + 1);
        }


        return _item;
    }

    public string FormattedBagLocation(InventoryItem inventoryItem)
    {
        if (inventoryItem.SortedContainer is InventoryType.GlamourChest or InventoryType.Currency or InventoryType.RetainerGil or InventoryType.FreeCompanyGil or InventoryType.Crystal or InventoryType.RetainerCrystal)
        {
            return SortedContainerName(inventoryItem);
        }
        return SortedContainerName(inventoryItem) + " - " + (inventoryItem.SortedSlotIndex + 1);
    }

    public string SortedContainerName(InventoryItem inventoryItem)
    {
        if(inventoryItem.SortedContainer is InventoryType.Bag0 or InventoryType.RetainerBag0)
        {
            return _localizationService.GetString("Item_Bag1");
        }
        if(inventoryItem.SortedContainer is InventoryType.Bag1 or InventoryType.RetainerBag1)
        {
            return _localizationService.GetString("Item_Bag2");
        }
        if(inventoryItem.SortedContainer is InventoryType.Bag2 or InventoryType.RetainerBag2)
        {
            return _localizationService.GetString("Item_Bag3");
        }
        if(inventoryItem.SortedContainer is InventoryType.Bag3 or InventoryType.RetainerBag3)
        {
            return _localizationService.GetString("Item_Bag4");
        }
        if(inventoryItem.SortedContainer is InventoryType.RetainerBag4)
        {
            return _localizationService.GetString("Item_Bag5");
        }
        if(inventoryItem.SortedContainer is InventoryType.SaddleBag0)
        {
            return _localizationService.GetString("Item_SaddlebagLeft");
        }
        if(inventoryItem.SortedContainer is InventoryType.SaddleBag1)
        {
            return _localizationService.GetString("Item_SaddlebagRight");
        }
        if(inventoryItem.SortedContainer is InventoryType.PremiumSaddleBag0)
        {
            return _localizationService.GetString("Item_PremiumSaddlebagLeft");
        }
        if(inventoryItem.SortedContainer is InventoryType.PremiumSaddleBag1)
        {
            return _localizationService.GetString("Item_PremiumSaddlebagRight");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryBody)
        {
            return _localizationService.GetString("Item_ArmoryBody");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryEar)
        {
            return _localizationService.GetString("Item_ArmoryEar");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryFeet)
        {
            return _localizationService.GetString("Item_ArmoryFeet");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryHand)
        {
            return _localizationService.GetString("Item_ArmoryHand");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryHead)
        {
            return _localizationService.GetString("Item_ArmoryHead");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryLegs)
        {
            return _localizationService.GetString("Item_ArmoryLegs");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryMain)
        {
            return _localizationService.GetString("Item_ArmoryMain");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryNeck)
        {
            return _localizationService.GetString("Item_ArmoryNeck");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryOff)
        {
            return _localizationService.GetString("Item_ArmoryOffhand");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryRing)
        {
            return _localizationService.GetString("Item_ArmoryRing");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryWaist)
        {
            return _localizationService.GetString("Item_ArmoryWaist");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmoryWrist)
        {
            return _localizationService.GetString("Item_ArmoryWrist");
        }
        if(inventoryItem.SortedContainer is InventoryType.ArmorySoulCrystal)
        {
            return _localizationService.GetString("Item_ArmorySoulCrystal");
        }
        if(inventoryItem.SortedContainer is InventoryType.GearSet0)
        {
            return _localizationService.GetString("Item_EquippedGear");
        }
        if(inventoryItem.SortedContainer is InventoryType.RetainerEquippedGear)
        {
            return _localizationService.GetString("Item_EquippedGear");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag0)
        {
            return _localizationService.GetString("Item_FreeCompanyChest1");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag1)
        {
            return _localizationService.GetString("Item_FreeCompanyChest2");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag2)
        {
            return _localizationService.GetString("Item_FreeCompanyChest3");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag3)
        {
            return _localizationService.GetString("Item_FreeCompanyChest4");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag4)
        {
            return _localizationService.GetString("Item_FreeCompanyChest5");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag5)
        {
            return _localizationService.GetString("Item_FreeCompanyChest6");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag6)
        {
            return _localizationService.GetString("Item_FreeCompanyChest7");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag7)
        {
            return _localizationService.GetString("Item_FreeCompanyChest8");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag8)
        {
            return _localizationService.GetString("Item_FreeCompanyChest9");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag9)
        {
            return _localizationService.GetString("Item_FreeCompanyChest10");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyBag10)
        {
            return _localizationService.GetString("Item_FreeCompanyChest11");
        }
        if(inventoryItem.SortedContainer is InventoryType.RetainerMarket)
        {
            return _localizationService.GetString("Item_Market");
        }
        if(inventoryItem.SortedContainer is InventoryType.GlamourChest)
        {
            return _localizationService.GetString("Item_GlamourChest");
        }
        if(inventoryItem.SortedContainer is InventoryType.Armoire)
        {
            return _localizationService.GetString("Item_Armoire") + " - " + CabinetName(inventoryItem);
        }
        if(inventoryItem.SortedContainer is InventoryType.Currency)
        {
            return _localizationService.GetString("Item_Currency");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyGil)
        {
            return _localizationService.GetString("Item_FreeCompanyGil");
        }
        if(inventoryItem.SortedContainer is InventoryType.RetainerGil)
        {
            return _localizationService.GetString("Item_Currency");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyCrystal)
        {
            return _localizationService.GetString("Item_FreeCompanyCrystals");
        }
        if(inventoryItem.SortedContainer is InventoryType.FreeCompanyCurrency)
        {
            return _localizationService.GetString("Item_FreeCompanyCurrency");
        }
        if(inventoryItem.SortedContainer is InventoryType.Crystal or InventoryType.RetainerCrystal)
        {
            return _localizationService.GetString("Item_Crystals");
        }
        if(inventoryItem.SortedContainer is InventoryType.HousingExteriorAppearance)
        {
            return _localizationService.GetString("Item_HousingExteriorAppearance");
        }
        if(inventoryItem.SortedContainer is InventoryType.HousingInteriorAppearance)
        {
            return _localizationService.GetString("Item_HousingInteriorAppearance");
        }
        if(inventoryItem.SortedContainer is InventoryType.HousingExteriorStoreroom or InventoryType.HousingExteriorStoreroom2)
        {
            return _localizationService.GetString("Item_HousingExteriorStoreroom");
        }
        if(inventoryItem.SortedContainer is InventoryType.HousingInteriorStoreroom1 or InventoryType.HousingInteriorStoreroom2 or InventoryType.HousingInteriorStoreroom3 or InventoryType.HousingInteriorStoreroom4 or InventoryType.HousingInteriorStoreroom5 or InventoryType.HousingInteriorStoreroom6 or InventoryType.HousingInteriorStoreroom7 or InventoryType.HousingInteriorStoreroom8 or InventoryType.HousingInteriorStoreroom9 or InventoryType.HousingInteriorStoreroom10 or InventoryType.HousingInteriorStoreroom11)
        {
            return _localizationService.GetString("Item_HousingInteriorStoreroom");
        }
        if(inventoryItem.SortedContainer is InventoryType.HousingInteriorPlacedItems1 or InventoryType.HousingInteriorPlacedItems2 or InventoryType.HousingInteriorPlacedItems3 or InventoryType.HousingInteriorPlacedItems4 or InventoryType.HousingInteriorPlacedItems5 or InventoryType.HousingInteriorPlacedItems6 or InventoryType.HousingInteriorPlacedItems7 or InventoryType.HousingInteriorPlacedItems8 or InventoryType.HousingInteriorPlacedItems9 or InventoryType.HousingInteriorPlacedItems10 or InventoryType.HousingInteriorPlacedItems11 or InventoryType.HousingInteriorPlacedItems12)
        {
            return _localizationService.GetString("Item_HousingInteriorPlacedItems");
        }
        if(inventoryItem.SortedContainer is InventoryType.HousingExteriorPlacedItems or InventoryType.HousingExteriorPlacedItems2)
        {
            return _localizationService.GetString("Item_HousingExteriorPlacedItems");
        }

        return inventoryItem.SortedContainer.ToString();
    }
}
