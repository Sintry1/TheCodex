interface IArmour {
    id: number;
    name: string;
    slot: string;
    type: string;
    effect?: string;
}

class Armour implements IArmour {
    id: number;
    name: string;
    slot: string;
    type: string;
    effect?: string;

    constructor(id: number, name: string, slot: string, type: string, effect: string) {
        this.id = id;
        this.name = name;
        this.slot = slot;
        this.type = type;
        this.effect = effect;
    }
}

module.exports = Armour;