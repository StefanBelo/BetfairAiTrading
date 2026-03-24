namespace Examples.FSharp

open System.Text.Json
open System.Text.Json.Serialization

module DTO =

	type Meta = {
		[<JsonPropertyName("status")>]
		status: int
		[<JsonPropertyName("code")>]
		code: string
	}

	type CountryCode = {
		[<JsonPropertyName("iso")>]
		iso: string option
		[<JsonPropertyName("racing")>]
		racing: string option
	}

	type Person = {
		[<JsonPropertyName("name")>]
		name: string option
		[<JsonPropertyName("slug")>]
		slug: string option
	}

	type Silk = {
		[<JsonPropertyName("press_association_id")>]
		press_association_id: string option
		[<JsonPropertyName("press_association_file")>]
		press_association_file: JsonElement option
	}

	type Runner = {
		[<JsonPropertyName("id")>]
		id: int
		[<JsonPropertyName("form")>]
		form: string option
		[<JsonPropertyName("equipment_text")>]
		equipment_text: string option
		[<JsonPropertyName("owner_name")>]
		owner_name: string option
		[<JsonPropertyName("format_weight")>]
		format_weight: string option
		[<JsonPropertyName("timeform_original_rating")>]
		timeform_original_rating: int option
		[<JsonPropertyName("horse_sex")>]
		horse_sex: string option
		[<JsonPropertyName("form_figure")>]
		form_figure: string option
		[<JsonPropertyName("jockey")>]
		jockey: Person option
		[<JsonPropertyName("trainer")>]
		trainer: Person option
		[<JsonPropertyName("silk")>]
		silk: Silk option
	}

	type FutureRunner = {
		[<JsonPropertyName("race")>]
		race: JsonElement option
		[<JsonPropertyName("jockey")>]
		jockey: JsonElement option
	}

	type Statistic = {
		[<JsonPropertyName("horse_race_type")>]
		horse_race_type: string option
		[<JsonPropertyName("total")>]
		total: int option
		[<JsonPropertyName("wins")>]
		wins: int option
		[<JsonPropertyName("seconds")>]
		seconds: int option
		[<JsonPropertyName("thirds")>]
		thirds: int option
		[<JsonPropertyName("fourths")>]
		fourths: int option
	}

	type StartingPrice = {
		[<JsonPropertyName("moneyline")>]
		moneyline: string option
	}

	type Performance = {
		[<JsonPropertyName("id")>]
		id: int option
		[<JsonPropertyName("meeting_date")>]
		meeting_date: string option
		[<JsonPropertyName("race_class")>]
		race_class: string option
		[<JsonPropertyName("group_text")>]
		group_text: string option
		[<JsonPropertyName("prize_fund_winner")>]
		prize_fund_winner: float option
		[<JsonPropertyName("finish_position")>]
		finish_position: int option
		[<JsonPropertyName("position_status")>]
		position_status: string option
		[<JsonPropertyName("race_runner_count")>]
		race_runner_count: int option
		[<JsonPropertyName("distance_beaten_text")>]
		distance_beaten_text: string option
		[<JsonPropertyName("distance_beaten_cumulative")>]
		distance_beaten_cumulative: string option
		[<JsonPropertyName("handicap")>]
		handicap: bool option
		[<JsonPropertyName("weight_text")>]
		weight_text: string option
		[<JsonPropertyName("race_distance")>]
		race_distance: int option
		[<JsonPropertyName("race_distance_formatted")>]
		race_distance_formatted: string option
		[<JsonPropertyName("race_start_time_scheduled")>]
		race_start_time_scheduled: string option
		[<JsonPropertyName("race_going")>]
		race_going: string option
		[<JsonPropertyName("race_going_official")>]
		race_going_official: string option
		[<JsonPropertyName("starting_price")>]
		starting_price: StartingPrice option
		[<JsonPropertyName("equipment_text")>]
		equipment_text: string option
		[<JsonPropertyName("race_type")>]
		race_type: string option
		[<JsonPropertyName("hi_lo_text")>]
		hi_lo_text: string option
		[<JsonPropertyName("rating_text")>]
		rating_text: string option
		[<JsonPropertyName("non_runner")>]
		non_runner: bool option
		[<JsonPropertyName("horse_race_type")>]
		horse_race_type: string option
		[<JsonPropertyName("owner_name")>]
		owner_name: string option
		[<JsonPropertyName("trainer_name")>]
		trainer_name: string option
		[<JsonPropertyName("comment_short")>]
		comment_short: string option
		[<JsonPropertyName("comment_full")>]
		comment_full: string option
		[<JsonPropertyName("track_name")>]
		track_name: string option
		[<JsonPropertyName("jockey")>]
		jockey: Person option
		[<JsonPropertyName("race")>]
		race: JsonElement option
		[<JsonPropertyName("track")>]
		track: JsonElement option
	}

	type Horse = {
		[<JsonPropertyName("id")>]
		id: int
		[<JsonPropertyName("name")>]
		name: string
		[<JsonPropertyName("country_code")>]
		country_code: CountryCode option
		[<JsonPropertyName("slug")>]
		slug: string option
		[<JsonPropertyName("gender")>]
		gender: string option
		[<JsonPropertyName("foaling_date")>]
		foaling_date: string option
		[<JsonPropertyName("age")>]
		age: int option
		[<JsonPropertyName("colors")>]
		colors: string list
		[<JsonPropertyName("display_colors")>]
		display_colors: string list
		[<JsonPropertyName("last_run_date")>]
		last_run_date: string option
		[<JsonPropertyName("days_since_last_run")>]
		days_since_last_run: int option
		[<JsonPropertyName("dam_name")>]
		dam_name: string option
		[<JsonPropertyName("dam_country_code")>]
		dam_country_code: string option
		[<JsonPropertyName("sire_name")>]
		sire_name: string option
		[<JsonPropertyName("sire_country_code")>]
		sire_country_code: string option
		[<JsonPropertyName("dam_sire_name")>]
		dam_sire_name: string option
		[<JsonPropertyName("dam_sire_country_code")>]
		dam_sire_country_code: string option
		[<JsonPropertyName("runner")>]
		runner: Runner option
		[<JsonPropertyName("future_runners")>]
		future_runners: FutureRunner list
		[<JsonPropertyName("statistics")>]
		statistics: Statistic list
		[<JsonPropertyName("performances")>]
		performances: Performance list
	}

	type RacingTvHorseDto = {
		[<JsonPropertyName("meta")>]
		meta: Meta option
		[<JsonPropertyName("horse")>]
		horse: Horse
	}

