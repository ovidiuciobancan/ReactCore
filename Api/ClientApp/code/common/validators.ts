import * as utils from './utils';

type BooleanFunction = (fieldValue: any) => boolean; 

const validation = (fieldValue: any, fieldName: string, errorMessage: string, isValid: BooleanFunction) => {
    let errors: any = {};

    if (isValid(fieldValue) === false) {
        errors[fieldName] = errorMessage;
    }

    return errors;
}

export const requiredType = (fieldValue: any, fieldName: string, fieldType: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Invalid type';
    let isValid = (fieldValue: any) => typeof (fieldValue) === fieldType;

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
export const requiredNumeric = (fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Value must be number';
    let isValid = (fieldValue: any) => /^[0-9.,]+$/.test(fieldValue);

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
//method that verifies if a (form)field (value=param1, name=param2[optional]) is a valid email address and
//returns an object with a key named 'email' by default (can be overridden with param2) and the value of the errorMessage 
export const requiredEmail = (fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Invalid email';
    let isValid = (fieldValue: any) => !fieldValue || /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(fieldValue);

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
//method that verifies any required fields = param2 in an array=param1 (ideally, this would be a form/redux-form) and 
//returns an object of errors where the keys are the names of the fields and the values are the errorMessages for each field; 
//TODO :if no second parameter is supplied, it should consider that all the fields in the form(all values) are required.
export const required = (fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || `${fieldName} is required`;
    let isValid = (fieldValue: any) => !!fieldValue;

    validation(fieldValue, fieldName, errorMessage, isValid);
}
//method that verifies if a (form)field is a valid CNP, first by length and then by actual calculus of the digits
//returns an object with a key named [fieldName] and the value of the errorMessage 
export const cnp = (fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Invalid CNP';
    let isValid = (fieldValue: any) => utils.checkCNP(fieldValue);

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
//method that verifies if a (form)field is a valid CUI, first by length and then by actual calculus of the digits
//returns an object with a key named [fieldName] and the value of the errorMessage
export const cui = (fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Invalid CUI';
    let isValid = (fieldValue: any) => utils.checkCUI(fieldValue);

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
//Methods that verifies if two strings(param1 and param2) exist and match
//returns an object with two keys named 'password' and 'confirmPassword' sharing the same errorMessage
export const passwordsMustMatch = (password: string, fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Password does not match';
    let isValid = (fieldValue: any) => (!!password && password === fieldValue);

    return !password || !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
//Method that verifies if a string(param1) is a valid phone number (between 10 and 12 digits)
//returns an object with a key named 'phone' by default (can be overridden with param2) and the value of the errorMessage 
export const phone = (fieldValue: string, fieldName: string, errorMessage?: string) => {
    errorMessage = errorMessage || 'Phone number is invalid';
    let isValid = (fieldValue: any) => /^\d{10,12}$/.test(fieldValue);

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}
//Methods that verifies if a password is complex enough
//^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,} -- has at least one uppercase, one lowercase, and one special char
//^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$  -- has at least one uppercase, one lowercase, one digit and one special char and at least 6 chars
export const complexPassword = (fieldValue: string, fieldName: string, errorMessage: string) => {
    errorMessage = errorMessage || 'Password is invalid';
    let isValid = (fieldValue: any) => /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$/i.test(fieldValue);

    return !fieldValue || validation(fieldValue, fieldName, errorMessage, isValid);
}







