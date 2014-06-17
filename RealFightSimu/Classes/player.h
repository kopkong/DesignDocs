#ifndef __PLAYER_H__
#define __PLAYER_H__

#include <string>

class Player
{
public:
	std::string m_Name;
	int m_Level;
	int m_EXP;
	int m_VIPLEVEL;

	// 当前背包里的游戏货币
	int m_CurrentCoin;

	// 当前背包里的ＲＭＢ货币
	int m_CurrentDiamonds;

	// 武将分解出来的魂，可以在神秘商店购买物品
	int m_CurrentHeroSouls;

	// 荣誉
	int m_Honor;

	// 公会声望
	int m_Fame;
	
	// 当前体力值
	int m_Energy;
	

	std::string displayName();

	void upgrade();

	void gainEXP(int);

	void promoteVIPLevel();

	void addCoin(int);

	void minusCoin(int);

	void addDiamonds(int);

	void minusDiamonds(int);

	void addHeroSouls(int);

	void minusHeroSouls(int);

	void addHonor(int);

	void minusHonor(int);

	void addFame(int);

	void minusFame(int);

	void autoRecoverEnergy();

	void addEnergy(int);

	void minusEnergy(int);
};


class PlayerDataMgr
{
private:
	Player* m_Player;

protected:
	~PlayerDataMgr();

public:
	static PlayerDataMgr& Instance();

	// 从文件初始化玩家数据
	void initPlayerData();

	// 保存用户数据到本地文件中
	void savePlayerData();
};
#endif