provider "aws" {
  region = var.aws_region
}

resource "aws_dynamodb_table" "users" {
  name         = "Users"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "Id"

  attribute {
    name = "Id"
    type = "S"
  }
}

resource "aws_dynamodb_table" "user_favorites" {
  name         = "UserFavorites"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "UserId"
  range_key    = "FavoriteIdRaw"

  attribute {
    name = "UserId"
    type = "S"
  }

  attribute {
    name = "FavoriteIdRaw"
    type = "S"
  }
}

resource "aws_dynamodb_table" "countries" {
  name         = "Countries"
  billing_mode = "PAY_PER_REQUEST"
  hash_key     = "CountryCode"

  attribute {
    name = "CountryCode"
    type = "S"
  }
}
