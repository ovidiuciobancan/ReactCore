//import colors from 'material-ui/styles/colors';
import colors from './colors';
import { fade } from 'material-ui/utils/colorManipulator';

export const appBaseTheme = {
    fontFamily: 'Open Sans, sans-serif',
    borderRadius: 5,
    palette: {
        primary1Color: colors.darkBlue,
        primary2Color: colors.pink100,
        primary3Color: colors.grey400,
        accent1Color: colors.pinkA200,
        accent2Color: colors.grey100,
        accent3Color: colors.grey500,
        textColor: colors.darkBlack,
        secondaryTextColor: fade(colors.darkBlack, 0.54),
        alternateTextColor: colors.white,
        canvasColor: colors.white,
        borderColor: colors.grey300,
        disabledColor:  fade(colors.darkBlack, 0.6),
        pickerHeaderColor: colors.cyan500,
        clockCircleColor: fade(colors.darkBlack, 0.07),
        shadowColor: colors.fullBlack
    },
};