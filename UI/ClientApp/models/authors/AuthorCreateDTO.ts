
import * as Models from 'models/index'

export interface IAuthorCreateDTO {
	firstName: string
	lastName: string
	dateOfBirth: Date
	dateofDeath: Date
	genre: string
	books: Models.IBookCreateDTO[],
	}

