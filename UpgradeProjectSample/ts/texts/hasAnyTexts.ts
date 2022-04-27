export function hasAnyTexts(value: string|null|undefined): value is string {
    if(value == null) {
        return false;
    }
    if(value.length <= 0) {
        return false;
    }
    return true;
}