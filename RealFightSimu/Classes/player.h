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

	// ��ǰ���������Ϸ����
	int m_CurrentCoin;

	// ��ǰ������ģңͣ»���
	int m_CurrentDiamonds;

	// �佫�ֽ�����Ļ꣬�����������̵깺����Ʒ
	int m_CurrentHeroSouls;

	// ����
	int m_Honor;

	// ��������
	int m_Fame;
	
	// ��ǰ����ֵ
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

	// ���ļ���ʼ���������
	void initPlayerData();

	// �����û����ݵ������ļ���
	void savePlayerData();
};
#endif