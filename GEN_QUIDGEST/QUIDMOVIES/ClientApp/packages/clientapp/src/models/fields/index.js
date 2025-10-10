import { Base } from './base.js'
import { Boolean } from './boolean.js'
import { Coordinate } from './coordinate.js'
import { Date } from './date.js'
import { DateTime } from './dateTime.js'
import { DateTimeSeconds } from './dateTimeSeconds.js'
import { Document } from './document.js'
import { DocumentData } from './documentData.js'
import { ForeignKey } from './foreignKey.js'
import { Geographic } from './geographic.js'
import { GridTableList } from './gridTableList.js'
import { Image } from './image.js'
import { MultiLineString } from './multiLineString.js'
import { MultipleValues } from './multipleValues.js'
import { Number } from './number.js'
import { Password } from './password.js'
import { PrimaryKey } from './primaryKey.js'
import { PropertyList } from './propertyList.js'
import { String } from './string.js'
import { Time } from './time.js'

export {
	Base,
	Boolean,
	Coordinate,
	Date,
	DateTime,
	DateTimeSeconds, // FIXME: this should not be exported, Document should suffice.
	Document,
	DocumentData,
	ForeignKey,
	Geographic,
	GridTableList,
	Image,
	MultiLineString,
	MultipleValues,
	Number,
	Password,
	PrimaryKey,
	PropertyList,
	String,
	Time
}

export default {
	Base,
	String,
	MultiLineString,
	Password,
	PrimaryKey,
	ForeignKey,
	Coordinate,
	Geographic,
	Date,
	DateTime,
	DateTimeSeconds,
	Time,
	Boolean,
	Number,
	Image,
	DocumentData, // FIXME: this should not be exported, Document should suffice.
	Document,
	MultipleValues,
	GridTableList,
	PropertyList
}
