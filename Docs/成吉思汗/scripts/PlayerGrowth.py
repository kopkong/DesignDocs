import random,csv
import collections
#Define all parameters
ONE_BATTLE_TIME = 60 #seconds

#We have properties
# 1 HP
# 2 ATK
# 3 DEF
# 4 CRITICAL
# 5 MISS
# 6 EXTRA_DAMAGEPLUS
# 7 EXTRA_DAMAGEMINUS

basicHeroGrowth = 100
basicEquipGrowth = 50
basicArmyGrowth = 50
basicTalentGrowth = 15
basicPeerageGrowth = 15 # Player's hero extra

highHeroGrowth = 120
highEquipGrowth = 75
highArmyGrowth = 75

onBattleSquads = {1:6,5:7,10:8,15:9,20:10,25:11,30:12,35:13,40:14,
				45:15,50:16,55:17,60:18,65:18,70:19,75:19,80:19,85:19,90:20,95:20,100:20}
od = collections.OrderedDict(sorted(onBattleSquads.items()))

def writeCSV(filename):
	ofile = open(filename,'wb')
	writer = csv.writer(ofile, dialect='excel')
	writer.writerow(['Column1', 'Column2', 'Column3'])
	lines = [range(3) for i in range(5)]
	for line in lines:
	    writer.writerow(line)
	ofile.close()

def getHP(level):
	initial = 250
	return initial + (level- 1) * 48

def getATK(level):
	initial = 35
	return initial + (level - 1) * 12

def getDEF(level):
	initial = 15
	return initial + (level - 1 ) * 3

def displayNobilityAttributes():
	allRanks = {10,15,30,45,60,75,90,105}
	allRanks = sorted(allRanks)
	for r in allRanks:
		print r
		print "%d, %d, %d" %(getHP(r),getATK(r),getDEF(r))

def displayNormal():
	for i in od:
		a1 = (basicHeroGrowth + basicArmyGrowth) * i *onBattleSquads[i]
		if i >= 10:
			a1 += basicEquipGrowth * i * onBattleSquads[i]
		if i >= 20:
			a1 += basicTalentGrowth * i * onBattleSquads[i]
		if i >= 30:
			a1 += basicPeerageGrowth * i * onBattleSquads[i]
		print a1

def displayHighEnd():
	for i in od:
		a2 = highHeroGrowth * i * onBattleSquads[i]
		if i >= 30:
			a2 += highArmyGrowth * i * onBattleSquads[i]
		else:
			a2 += basicArmyGrowth * i * onBattleSquads[i]

		if i >= 10:
			a2 += highEquipGrowth * i * onBattleSquads[i]

		if i >= 20:
			a2 += basicTalentGrowth * i * onBattleSquads[i]

		if i >= 30:
			a2 += basicPeerageGrowth * i * onBattleSquads[i]

		print a2

def displayDifficulty():
	for i in od:
		d = round(100 * pow(i,1.3)) * (onBattleSquads[i] - 1)
		print d 

if __name__ == '__main__':
	#displayHighEnd()
	displayNobilityAttributes()
	