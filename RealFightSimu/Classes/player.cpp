#include "player.h"
#include "cocos2d.h"
#include "playerGlobalConfig.h"

void Player::addCoin(int coin)
{
	CC_ASSERT(coin >0 && "Coin must bigger than 0");

	int newCoins = m_CurrentCoin + coin;
	if( newCoins < PLAYER_MAX_COIN)
	{
		m_CurrentCoin = newCoins;
	}

}

void Player::minusCoin(int coin)
{
	CC_ASSERT(coin >0 && "Coin must bigger than 0");

	int newCoins = m_CurrentCoin - coin;
	CC_ASSERT(newCoins >=0 && "Player's coins can't be negetive!");

	m_CurrentCoin = newCoins;
}

void Player::addDiamonds(int diamond)
{
	CC_ASSERT(diamond >0 && "Diamond must bigger than 0");
	int newValue = m_CurrentDiamonds + diamond;

	m_CurrentDiamonds = newValue;
}

void Player::minusDiamonds(int diamond)
{
	CC_ASSERT(diamond >0 && "Diamond must bigger than 0");

	int newValue = m_CurrentDiamonds - diamond;
	CC_ASSERT(newValue >=0 && "Player's diamonds can't be negetive!");

	m_CurrentDiamonds = newValue;

}

void Player::gainEXP(int exp)
{
	CC_ASSERT(exp > 0 && "EXP must bigger than 0");

	int totalExp = m_EXP + exp;

	m_EXP = totalExp;

	// upgrade
	if(totalExp >= PLAYER_ACCUMULATION_EXPERINCE_ARRAY[m_Level])
	{
		upgrade();
	}
}

void Player::upgrade()
{
	if(m_Level< PLAYER_MAX_LEVEL)
	{
		// Notify player upgrade

		// Éý¼¶
		m_Level = m_Level + 1 ;
	}
}

void Player::addEnergy(int energy)
{
	CC_ASSERT(energy > 0 && "ENerty must bigger than 0");

	int newValue = m_Energy + energy;

	m_Energy = newValue;
}

void Player::autoRecoverEnergy()
{
	int newValue = m_Energy + ENERGY_AUTOINCRESE_AMOUNT;

	if(newValue> PLAYER_MAX_AUTOINCRESE_ENERGY)
	{
		newValue = PLAYER_MAX_AUTOINCRESE_ENERGY;
	}

	m_Energy = newValue;
}
