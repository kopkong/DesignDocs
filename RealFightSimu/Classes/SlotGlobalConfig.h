//
//  slotGlobalConfig.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#ifndef RealFightSimu_slotGlobalConfig_h
#define RealFightSimu_slotGlobalConfig_h

#include <string>

static const int PLAYER_MAX_HERO_SLOTS = 100;
static const int PLAYER_MAX_ITEM_SLOTS = 100;
static const int PLAYER_MAX_ARMOR_SLOTS = 100;
static const int PLAYER_MAX_ARMY_SLOTS = 100;
static const int PLAYER_MAX_ARMORMAT_SLOTS = 100;
static const int PLAYER_MAX_ARMYMAT_SLOTS = 100;

static const char* SLOT_DATAKEY_HEROSIZE = "SLOTHEROSIZE";
static const char* SLOT_DATAKEY_ITEMSIZE = "SLOTITEMSIZE";
static const char* SLOT_DATAKEY_ARMORSIZE = "SLOTARMORSIZE";
static const char* SLOT_DATAKEY_ARMYSIZE = "SLOTARMYSIZE";
static const char* SLOT_DATAKEY_ARMORMATSIZE = "SLOTARMORMATSIZE";
static const char* SLOT_DATAKEY_ARMYMATSIZE = "SLOTARMYMATSIZE";
static const char* SLOT_DATAKEY_FORMATIONSIZE = "SLOTFORMATIONSIZE";

static const char* SLOT_DATAKEY_HERODATA = "SLOTHERODATA";
static const char* SLOT_DATAKEY_ITEMDATA = "SLOTITEMDATA";
static const char* SLOT_DATAKEY_ARMORDATA = "SLOTARMORDATA";
static const char* SLOT_DATAKEY_ARMYDATA = "SLOTARMYDATA";
static const char* SLOT_DATAKEY_ARMORMATDATA = "SLOTARMORMATDATA";
static const char* SLOT_DATAKEY_ARMYMATDATA = "SLOTARMYMATDATA";


static const std::string PLAYER_DEFAULT_HERODATASTRING = "1,1,1,1,1";
static const int SLOTHERO_ONFIELD = 1;
static const int SLOTHERO_OFFFIELD = 0;

enum SLOTTYPE
{
    HERO,
    ITEM,
    ARMOR,
    ARMY,
    ARMORMAT,
    ARMYMAT,
    FORMATION,
    ALL
};

enum SLOTINDEX
{
    NOSLOT,
    SLOT1,
    SLOT2,
    SLOT3,
    SLOT4,
    SLOT5,
    SLOT6,
    SLOT7,
    SLOT8,
    SLOT9,
    SLOT10,
    SLOT11,
    SLOT12,
    SLOT13,
    SLOT14,
    SLOT15,
    SLOT16,
    SLOT17,
    SLOT18,
    SLOT19,
    SLOT20,
    SLOT21,
    SLOT22,
    SLOT23,
    SLOT24,
    SLOT25,
    SLOT26,
    SLOT27,
    SLOT28,
    SLOT29,
    SLOT30,
    SLOT31,
    SLOT32,
    SLOT33,
    SLOT34,
    SLOT35,
    SLOT36,
    SLOT37,
    SLOT38,
    SLOT39,
    SLOT40,
    SLOT41,
    SLOT42,
    SLOT43,
    SLOT44,
    SLOT45,
    SLOT46,
    SLOT47,
    SLOT48,
    SLOT49,
    SLOT50,
    SLOT51,
    SLOT52,
    SLOT53,
    SLOT54,
    SLOT55,
    SLOT56,
    SLOT57,
    SLOT58,
    SLOT59,
    SLOT60,
    SLOT61,
    SLOT62,
    SLOT63,
    SLOT64,
    SLOT65,
    SLOT66,
    SLOT67,
    SLOT68,
    SLOT69,
    SLOT70,
    SLOT71,
    SLOT72,
    SLOT73,
    SLOT74,
    SLOT75,
    SLOT76,
    SLOT77,
    SLOT78,
    SLOT79,
    SLOT80,
    SLOT81,
    SLOT82,
    SLOT83,
    SLOT84,
    SLOT85,
    SLOT86,
    SLOT87,
    SLOT88,
    SLOT89,
    SLOT90,
    SLOT91,
    SLOT92,
    SLOT93,
    SLOT94,
    SLOT95,
    SLOT96,
    SLOT97,
    SLOT98,
    SLOT99,
    SLOT100,
    MAXSLOT
};

#endif
