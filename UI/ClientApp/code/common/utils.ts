//generates unique id
export const guid = () => {
    let s4 = () => {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

//regex
export const regexGroup = (pattern: RegExp, value: string): string => {
    return regexGroups(pattern, value)[0];
}
export const regexGroups = (pattern: RegExp, value: string): string[] => {
    return new RegExp(pattern)
        .exec(value) || [];
}

//format checkers
export const checkCNP = (cnp: string): boolean => {
    let CNP_S =  parseInt(cnp[0]);
    let CNP_A1 = parseInt(cnp[1]);
    let CNP_A2 = parseInt(cnp[2]);
    let CNP_L1 = parseInt(cnp[3]);
    let CNP_L2 = parseInt(cnp[4]);
    let CNP_Z1 = parseInt(cnp[5]);
    let CNP_Z2 = parseInt(cnp[6]);
    let CNP_N1 = parseInt(cnp[7]);
    let CNP_N2 = parseInt(cnp[8]);
    let CNP_N3 = parseInt(cnp[9]);
    let CNP_N4 = parseInt(cnp[10]);
    let CNP_N5 = parseInt(cnp[11]);
    let CNP_C =  parseInt(cnp[12]);

    let sum = CNP_S * 2 +
        CNP_A1 * 7 +
        CNP_A2 * 9 +
        CNP_L1 * 1 +
        CNP_L2 * 4 +
        CNP_Z1 * 6 +
        CNP_Z2 * 3 +
        CNP_N1 * 5 +
        CNP_N2 * 8 +
        CNP_N3 * 2 +
        CNP_N4 * 7 +
        CNP_N5 * 9;

    let rest = sum % 11;
    if ((rest < 10) && (rest == CNP_C) || (rest == 10) && (CNP_C == 1)) {
        return true;
    }

    return false;
}
export const checkCUI = (cui: string): boolean => {
    if (cui.length === 12 && cui.substr(0, 2).toLowerCase() === 'ro') {
        cui = cui.substr(2, 10);
    }

    if (cui.length > 10 || cui.length < 4) {
        return false;
    }

    let cod = 753217532;
    let control = cui.substr(cui.length - 1, 1);
    cui = cui.substr(0, cui.length - 1);

    let sum = 7 * parseInt(cui[0]) +
        5 * parseInt(cui[1]) +
        3 * parseInt(cui[2]) +
        2 * parseInt(cui[3]) +
            parseInt(cui[4] ? cui[4] : '0') +
        7 * parseInt(cui[5] ? cui[5] : '0') +
        5 * parseInt(cui[6] ? cui[6] : '0') +
        3 * parseInt(cui[7] ? cui[7] : '0') +
        2 * parseInt(cui[8] ? cui[8] : '0');

    let check = (sum * 10) % 11;
    if (check == 10) {
        check = 0;
    }

    return check == parseInt(control);
}

//formatters
export const moneyFormater = (value: any, currency?: string): string => {
    if (!+value) return '';
    let formatedValue = decimalFormatter(value);
    return `${formatedValue} ${currency}`;
}
export const decimalFormatter = (value: number) => {
    return value.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
};