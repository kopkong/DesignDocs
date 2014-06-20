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

	// 当前的金钱
	int m_CurrentCoin;

	// 钻石
	int m_CurrentDiamonds;

	// Œ‰Ω´∑÷Ω‚≥ˆ¿¥µƒªÍ£¨ø…“‘‘⁄…Ò√ÿ…ÃµÍπ∫¬ÚŒÔ∆∑
	int m_CurrentHeroSouls;

	// »Ÿ”˛
	int m_Honor;

	// π´ª·…˘Õ˚
	int m_Fame;
	
	// µ±«∞ÃÂ¡¶÷µ
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

	// ¥”Œƒº˛≥ı ºªØÕÊº“ ˝æ›
	void initPlayerData();

	// ±£¥Ê”√ªß ˝æ›µΩ±æµÿŒƒº˛÷–
	void savePlayerData();
};
#endif